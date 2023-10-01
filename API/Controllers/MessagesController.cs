using API.Controllers;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace API;

public class MessagesController : BaseApiController
{
    private readonly IUnitOfWork uow;
    private readonly IMapper mapper;

    public MessagesController(IUnitOfWork uow, IMapper mapper)
    {
        this.uow = uow;
        this.mapper = mapper;
    }

    [HttpPost]
    public async Task<ActionResult<MessageDto>> CreateMessage(CreateMessageDto createMessageDto)
    {
        var username = User.GetUsername();
        if (username == createMessageDto.RecipientUsername.ToLower())
            return BadRequest("You cannot send messages to yourself");

        var sender = await uow.UserRepository.GetUserByUserNameAsync(username);
        var recipient = await uow.UserRepository.GetUserByUserNameAsync(createMessageDto.RecipientUsername);

        if (recipient == null) return NotFound();

        var message = new Message
        {
            Sender = sender,
            Recipient = recipient,
            SenderUsername = sender.UserName,
            RecipientUsername = recipient.UserName,
            Content = createMessageDto.Content
        };

        uow.MessageRepository.AddMessage(message);
        if (await uow.Complete())
            return Ok(mapper.Map<MessageDto>(message));

        return BadRequest("Failed to send message");
    }

    [HttpGet]
    public async Task<ActionResult<PagedList<MessageDto>>> GetMessagesForUser([FromQuery] MessageParams messageParams)
    {
        messageParams.Username = User.GetUsername();
        var messages = await uow.MessageRepository.GetMessagesForUser(messageParams);
        Response.AddPaginationHeader(new PaginationHeader(messages.CurrentPage, messages.PageSize, messages.TotalCount, messages.TotalPages));

        return messages;
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteMessage(int id)
    {
        var username = User.GetUsername();
        var message = await uow.MessageRepository.GetMessage(id);
        if (username != message.SenderUsername && username != message.RecipientUsername) return Unauthorized();

        if (message.SenderUsername == username)
            message.SenderDeleted = true;
        if (message.RecipientUsername == username)
            message.RecipientDeleted = true;
        if (message.SenderDeleted && message.RecipientDeleted)
            uow.MessageRepository.DeleteMessage(message);
        if (await uow.Complete())
            return Ok();
        return BadRequest("Problem deleting message");

    }
}
