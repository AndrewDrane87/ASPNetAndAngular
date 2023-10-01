using System.Security.Claims;
using API.Data;
using API.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

[Authorize]
public class UsersController : BaseApiController
{
    private readonly IUnitOfWork uow;
    private readonly IMapper mapper;
    private readonly IPhotoService photoService;

    public UsersController(IUnitOfWork uow, IMapper mapper, IPhotoService photoService)
    {
        this.uow = uow;
        this.mapper = mapper;
        this.photoService = photoService;
    }

    [HttpGet]
    public async Task<ActionResult<PagedList<MemberDto>>> GetUsers([FromQuery]UserParams userParams)
    {
        var gender = await uow.UserRepository.GetUserGender(User.GetUsername());
        userParams.CurrentUserName = User.GetUsername();
        if(string.IsNullOrEmpty(userParams.Gender)){
            userParams.Gender = gender == "male" ? "female":"male";
        }

        var users = await uow.UserRepository.GetMembersAsync(userParams);
        Response.AddPaginationHeader(new PaginationHeader(users.CurrentPage, users.PageSize,users.TotalCount,users.TotalPages));
        return Ok(users);
    }


    [HttpGet("{username}")] //api/users/2
    public async Task<ActionResult<MemberDto>> GetUser(string userName)
    {
        return await uow.UserRepository.GetMemberAsync(userName);
    }

    [HttpPut]
    public async Task<ActionResult> UpdateUser(MemberUpdateDto memberUpdateDto)
    {
        var user = await uow.UserRepository.GetUserByUserNameAsync(User.GetUsername());

        if (user == null) return NotFound();
        //Use auto mapper to assign all properties from memberdto from client to the db object we just got
        mapper.Map(memberUpdateDto, user);

        if (await uow.Complete())
            return NoContent();
        else
            return BadRequest("Failed to update user");

    }

    [HttpPost("add-photo")]
    public async Task<ActionResult<PhotoDto>> AddPhoto(IFormFile file)
    {
        var user = await uow.UserRepository.GetUserByUserNameAsync(User.GetUsername());

        if (user == null) return NotFound();

        var result = await photoService.AddPhotoAsync(file);
        if (result.Error != null) return BadRequest(result);

        var photo = new Photo
        {
            Url = result.SecureUrl.AbsoluteUri,
            PublicId = result.PublicId
        };

        //Check to see if its the users first photo, set to main
        if (user.Photos.Count == 0)
        {
            photo.IsMain = true;
        }

        user.Photos.Add(photo);
        if (await uow.Complete())
        {
            //Incorrect response for a rest api
            //return mapper.Map<PhotoDto>(photo);
            return CreatedAtAction(nameof(GetUser), new { username = user.UserName }, mapper.Map<PhotoDto>(photo));
        }

        return BadRequest("Error adding photo");
    }

    [HttpPut("set-main-photo/{photoId}")]
    public async Task<ActionResult> SetMainPhoto(int photoId)
    {
        var user = await uow.UserRepository.GetUserByUserNameAsync(User.GetUsername());
        if (user == null) return NotFound();

        var photo = user.Photos.FirstOrDefault(x => x.Id == photoId);

        if (photo == null) return NotFound();

        if (photo.IsMain) return BadRequest("This is already your main photo");

        var currentMainPhoto = user.Photos.FirstOrDefault(x => x.IsMain);
        if (currentMainPhoto != null) currentMainPhoto.IsMain = false;
        photo.IsMain = true;

        if (await uow.Complete()) return NoContent();
        return BadRequest("Error setting main photo");
    }

    [HttpDelete("delete-photo/{photoId}")]
    public async Task<ActionResult> DeletePhoto(int photoId)
    {
        var user = await uow.UserRepository.GetUserByUserNameAsync(User.GetUsername());

        var photo = user.Photos.FirstOrDefault(x => x.Id == photoId);
        if (photo == null) return NotFound();
        if (photo.IsMain) return BadRequest("Cannot delete main photo");
        //Make sure this isnt one of our seeded images that doesnt exist in cloudinary
        if (photo.PublicId != null)
        {
            var result = await photoService.DeletePhotoAsync(photo.PublicId);
            if(result.Error != null)return BadRequest(result.Error.Message);
        }
        user.Photos.Remove(photo);
        if(await uow.Complete()){
            return Ok();
        }

        return BadRequest("Problem deleting photo");
    }
}

