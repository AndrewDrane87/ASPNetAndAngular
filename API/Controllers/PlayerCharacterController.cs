using API.Controllers;
using API.DTOs.Items;
using API.Entities;
using Microsoft.AspNetCore.Mvc;

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
            Helmet = c.Helmet,
            LeftHand = c.Shield,
            RightHand =c.Sword,
            Body = c.Armor,
            Feet = c.Boots,
        });

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
    public async Task<ActionResult<List<Item>>> GetAvailableHandItems([FromQuery] string type, [FromQuery] int characterId)
    {
        
        switch (type)
        {
            case "helmets": return Ok(await uow.AdventureRepository.GetItems(characterId, "helmet"));
            case "hand-items": return Ok(await uow.AdventureRepository.GetItems(characterId, "hand"));
            case "armor": return Ok(await uow.AdventureRepository.GetItems(characterId, "armor"));
            case "boots": return Ok(await uow.AdventureRepository.GetItems(characterId, "boot"));
            default: return BadRequest("You must specify an item type");
        }

    }

    [HttpPut("set-character-item")]
    public async Task<ActionResult> SetCharacterItem(SetItemDto set)
    {
        bool status = false;
        switch (set.ItemType)
        {
            case "helmets":
                status = await uow.PlayerCharacterRepository.SetHelmet(set.CharacterId, set.ItemId);
                break;
            case "leftHand":
                status = await uow.PlayerCharacterRepository.SetLeftHand(set.CharacterId, set.ItemId);
                break;
            case "rightHand":
                status = await uow.PlayerCharacterRepository.SetRightHand(set.CharacterId, set.ItemId);
                break;
            case "armor":
                status = await uow.PlayerCharacterRepository.SetArmor(set.CharacterId, set.ItemId);
                break;
            case "boots":
                status = await uow.PlayerCharacterRepository.SetBoots(set.CharacterId, set.ItemId);
                break;
        }
        if (status)
        {
            if (await uow.Complete()) {
                Console.WriteLine("set-character-item: result: OK");
                return Ok();
            }
            
        }

        return BadRequest("Failed to save item selection");
    }
}
