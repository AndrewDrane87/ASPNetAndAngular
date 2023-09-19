using API.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace API;

public class LikesController : BaseApiController
{
    private readonly IUserRepository userRepository;
    private readonly ILikesRepository likesRepository;

    public LikesController(IUserRepository userRepository, ILikesRepository likesRepository)
    {
        this.userRepository = userRepository;
        this.likesRepository = likesRepository;
    }

    [HttpPost("{username}")]
    public async Task<ActionResult> AddLike(string username)
    {
        var sourceUserId = User.GetUserId();
        var likedUser = await userRepository.GetUserByUserNameAsync(username);
        var sourceUser = await likesRepository.GetUserWithLikes(sourceUserId);

        if (likedUser == null) return NotFound();

        //If you try to like your own user
        if (sourceUser.UserName == username) return BadRequest("You cannot like yourself");

        //Check to see if the user has already been liked        
        var userLike = await likesRepository.GetUserLike(sourceUserId, likedUser.Id);
        if (userLike != null) return BadRequest("You already like this user");

        userLike = new UserLike
        {
            SourceUserId = sourceUserId,
            TargetUserId = likedUser.Id
        };
        sourceUser.LikedUsers.Add(userLike);

        if (await userRepository.SaveAllAsync()) return Ok();

        return BadRequest("Failed to like user");
    }

    [HttpGet]
    public async Task<ActionResult<PagedList<LikeDto>>> GetUserLikes([FromQuery] LikesParams likesParams)
    {
        likesParams.UserId = User.GetUserId();
        var users = await likesRepository.GetUserLikes(likesParams);

        Response.AddPaginationHeader(new PaginationHeader(users.CurrentPage, users.PageSize, users.TotalCount, users.TotalPages));
        return Ok(users);
    }
}
