using API.DTOs.Admin;
using API.Entities;
using API.Entities.Adventure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

public class AdminController : BaseApiController
{
    private readonly UserManager<AppUser> userManager;
    private readonly UnitOfWork uow;

    public AdminController(UserManager<AppUser> userManager, UnitOfWork uow)
    {
        this.userManager = userManager;
        this.uow = uow;
    }

    [Authorize(Policy = "RequireAdminRole")]
    [HttpGet("users-with-roles")]
    public async Task<ActionResult> GetUsersWithRoles()
    {
        var users = await userManager.Users
        .OrderBy(u => u.UserName)
        .Select(u => new
        {
            u.Id,
            Username = u.UserName,
            Roles = u.UserRoles.Select(r => r.Role.Name)
        }).ToListAsync();

        return Ok(users);
    }

    [Authorize(Policy = "RequireAdminRole")]
    [HttpPost("edit-roles/{username}")] //To be super accurate this might be a put since we are updating a thing
    public async Task<ActionResult> EditRoles(string username, [FromQuery] string roles)
    {
        if (string.IsNullOrEmpty(roles)) return BadRequest("You must select at least one role");

        var selectedRoles = roles.Split(',').ToArray();
        var user = await userManager.FindByNameAsync(username);
        if (user == null) return NotFound();

        var userRoles = await userManager.GetRolesAsync(user);

        var result = await userManager.AddToRolesAsync(user, selectedRoles.Except(userRoles));
        if (!result.Succeeded) return BadRequest("Failed to add to roles");

        result = await userManager.RemoveFromRolesAsync(user, userRoles.Except(selectedRoles));
        if (!result.Succeeded) return BadRequest("Failed to remove from roles");

        return Ok(await userManager.GetRolesAsync(user));
    }

    [Authorize(Policy = "ModeratePhotoRole")]
    [HttpGet("photos-to-moderate")]
    public ActionResult GetPhotosForModeration() { return Ok("Admins or moderators can see this"); }

    [HttpGet("get-adventure")]
    public async Task<ActionResult<AdminAdventureDto>> GetAdventure([FromQuery] int id)
    {
        var adventure =  await uow.AdventureRepository.GetAdventureForAdmin(id);
        if(adventure == null) return NotFound();

        return Ok(adventure);
    }
}
