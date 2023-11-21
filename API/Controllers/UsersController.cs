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
    private readonly UnitOfWork uow;
    private readonly IMapper mapper;
    private readonly IPhotoService photoService;

    public UsersController(UnitOfWork uow, IMapper mapper, IPhotoService photoService)
    {
        this.uow = uow;
        this.mapper = mapper;
        this.photoService = photoService;
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
}

