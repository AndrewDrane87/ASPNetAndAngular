using API.Controllers;
using API.DTOs.Items;
using API.Entities;
using API.Helpers;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Eventing.Reader;

namespace API;


public class PlayerCharactersController : BaseApiController
{
    private readonly UnitOfWork uow;

    public PlayerCharactersController(UnitOfWork uow)
    {
        this.uow = uow;
    }

    [HttpPost("create-player-character")] //post: playercharacter/create
    public async Task<ActionResult<PlayerCharacter>> Create(CreatePlayerCharacterDto playerCharacter)
    {

        BasicItemCollection c = await uow.ItemRepository.GetBasicItems();
        var newPc = uow.PlayerCharacterRepository.Create(new PlayerCharacter()
        {
            Name = playerCharacter.Name,
            PhotoUrl = playerCharacter.PhotoUrl,

        });
        newPc.Helmet = new ItemSave { Item = c.Helmet };
        newPc.LeftHand = new ItemSave { Item = c.Shield };
        newPc.RightHand = new ItemSave { Item = c.Sword };
        newPc.Body = new ItemSave { Item = c.Armor };
        newPc.Feet = new ItemSave { Item = c.Boots };

        var user = await uow.UserRepository.GetUserByUserNameAsync(User.GetUsername());
        user.MyCharacters.Add(newPc);
        if (await uow.Complete())
            return Ok(newPc);

        return BadRequest("Failed to send message");
    }



    [HttpGet]
    public async Task<ActionResult<List<PlayerCharacter>>> GetPlayerCharactersForUser([FromQuery] PlayerCharacterParams queryParams)
    {
        var user = await uow.UserRepository.GetUserByUserNameAsync(User.GetUsername());
        var characters = user.MyCharacters;
        return Ok(characters);
    }

    [HttpGet("get-player-character/{id}")]
    public async Task<ActionResult<PlayerCharacterDto>> GetPlayerCharacter(int id)
    {
        var user = await uow.UserRepository.GetUserByUserNameAsync(User.GetUsername());
        var pc = user.MyCharacters.Where(pc => pc.Id == id).FirstOrDefault();
        var character = await uow.PlayerCharacterRepository.GetCharacterByPlayerAndIdAsync(id);
        if (character == null) return BadRequest("Character not found");
        if (pc == null) return BadRequest("Character is not owned by user");
        return Ok(character);
    }

    [HttpDelete("delete-player-character/{id}")]
    public async Task<ActionResult> DeletePlayerCharacterById(int id)
    {
        var name = uow.PlayerCharacterRepository.Remove(id);
        if (await uow.Complete())
            return Ok($"Player Character: {name} was deleted successfully");

        return BadRequest("Failed to delete character");
    }


    [HttpGet("get-available-items")]
    public async Task<ActionResult<List<ItemSaveDto>>> GetAvailableItems([FromQuery] string type, [FromQuery] int characterId, [FromQuery] int currentItemId = -1)
    {

        switch (type)
        {
            case "helmet": return Ok(await uow.AdventureRepository.GetItems(characterId, "helmet"));
            case "hand": return Ok(await uow.AdventureRepository.GetItems(characterId, "hand"));
            case "armor": return Ok(await uow.AdventureRepository.GetItems(characterId, "armor"));
            case "boot": return Ok(await uow.AdventureRepository.GetItems(characterId, "boot"));
            case "any": return Ok(await uow.AdventureRepository.GetItems(characterId,"any", currentItemId));
            default: return BadRequest("You must specify an item type");
        }
    }

    [HttpPut("set-character-item")]
    public async Task<ActionResult> SetCharacterItem(SetItemDto set)
    {
        StatusMessage statusMessage = new StatusMessage() { Status = false};
        if (set.ItemType == "backpack")
            statusMessage = await uow.PlayerCharacterRepository.SetBackpack(set.CharacterId, set.ItemId, set.BackpackIndex);
        else
            statusMessage = await uow.PlayerCharacterRepository.SetCharacterItem(set.CharacterId, set.ItemId, set.ItemType);
        
        if (statusMessage.Status)
        {
            Console.WriteLine("set-character-item: result: OK");
            return Ok();
        }
        return BadRequest("Failed to save item selection");
    }

    [HttpPut("use-item")]
    public async Task<ActionResult> UseItem([FromQuery] int playerCharacterId, [FromQuery] int ItemId)
    {
        var statusMessage = await uow.PlayerCharacterRepository.UseItem(playerCharacterId, ItemId);
        if(statusMessage.Status)
            return Ok();

        return BadRequest(statusMessage.Message);
    }
}
