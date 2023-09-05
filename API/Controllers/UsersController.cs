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
    private readonly IUserRepository userRepository;
    private readonly IMapper mapper;

    public UsersController(IUserRepository userRepository, IMapper mapper)
    {
        this.userRepository = userRepository;
        this.mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<MemberDto>>> GetUsers()
    {
        var users = await userRepository.GetMembersAsync();
        return Ok(users);
    }


    [HttpGet("{username}")] //api/users/2
    public async Task<ActionResult<MemberDto>> GetUser(string userName)
    {
        return await userRepository.GetMemberAsync(userName);
    }

    [HttpPut]
    public async Task<ActionResult> UpdateUser(MemberUpdateDto memberUpdateDto)
    {
        var username = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var user = await userRepository.GetUserByUserNameAsync(username);

        if (user == null) return NotFound();
        //Use auto mapper to assign all properties from memberdto from client to the db object we just got
        mapper.Map(memberUpdateDto, user);

        if (await userRepository.SaveAllAsync())
            return NoContent();
        else
            return BadRequest("Failed to update user");

    }
}

