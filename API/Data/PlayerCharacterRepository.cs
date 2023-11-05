
using API.Data;
using API.Entities;
using API.Helpers;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace API;

public class PlayerCharacterRepository : IPlayerCharacterRepository
{
    private readonly DataContext context;
    private readonly IMapper mapper;

    public PlayerCharacterRepository(DataContext context, IMapper mapper)
    {
        this.context = context;
        this.mapper = mapper;
    }

    public PlayerCharacter Create(PlayerCharacter pc)
    {
        context.PlayerCharacters.Add(pc);
        context.SaveChanges();
        return pc;
    }

    public async Task<ICollection<PlayerCharacter>> GetCharactersForUser(string username)
    {
        Console.WriteLine("You made it here");
        var user = await context.Users.Where(u => u.UserName == username)
        .Include(u => u.MyCharacters)
        .FirstOrDefaultAsync();
        return user.MyCharacters;
    }

    public async Task<PlayerCharacterDto> GetCharacterByPlayerAndIdAsync(int characterId)
    {
        var pc = await context.PlayerCharacters.Where(p => p.Id == characterId)
            .Include(p => p.Helmet).ThenInclude(helmet => helmet.Photo)
            .Include(p => p.LeftHand).ThenInclude(leftHand => leftHand.Photo)
            .Include(p => p.RightHand).ThenInclude(rightHand => rightHand.Photo)
            .Include(p => p.Body).ThenInclude(body => body.Photo)
            .Include(p => p.Feet).ThenInclude(feet => feet.Photo)
            .FirstOrDefaultAsync();

        var pcDto = new PlayerCharacterDto()
        {
            Id = characterId,
            Name = pc.Name,
            PhotoUrl = pc.PhotoUrl,
            Helmet = DTOConversion.ConvertItem(pc.Helmet),
            LeftHand = DTOConversion.ConvertItem(pc.LeftHand),
            Body = DTOConversion.ConvertItem(pc.Body),
            RightHand = DTOConversion.ConvertItem(pc.RightHand),
            Feet = DTOConversion.ConvertItem(pc.Feet),
        };
        return pcDto;
    }

    public string Remove(int id)
    {
        var name = string.Empty;
        var pc = context.PlayerCharacters.Where(p => p.Id == id).FirstOrDefault();
        if (pc != null)
        {
            name = pc.Name;
            context.PlayerCharacters.Remove(pc);
        }
        return name;
    }
    
    public async Task<bool> SetHelmet(int characterId, int itemId)
    {
        var pc = await context.PlayerCharacters.Where(p => p.Id == characterId).FirstOrDefaultAsync();
        var item = await context.ItemCollection.Where(h => h.Id == itemId).FirstOrDefaultAsync();
        if (pc == null || item == null) return false;

        pc.Helmet = item;

        return true;
    }
    
    public async Task<bool> SetLeftHand(int characterId, int itemId)
    {
        var pc = await context.PlayerCharacters.Where(p => p.Id == characterId).FirstOrDefaultAsync();
        var item = await context.ItemCollection.Where(h => h.Id == itemId).FirstOrDefaultAsync();
        if (pc == null || item == null) return false;

        pc.LeftHand = item;

        return true;
    }
    
    public async Task<bool> SetRightHand(int characterId, int itemId)
    {
        var pc = await context.PlayerCharacters.Where(p => p.Id == characterId).FirstOrDefaultAsync();
        var item = await context.ItemCollection.Where(h => h.Id == itemId).FirstOrDefaultAsync();
        if (pc == null || item == null) return false;

        pc.RightHand = item;

        return true;
    }
    
    public async Task<bool> SetArmor(int characterId, int itemId)
    {
        var pc = await context.PlayerCharacters.Where(p => p.Id == characterId).FirstOrDefaultAsync();
        var item = await context.ItemCollection.Where(h => h.Id == itemId).FirstOrDefaultAsync();
        if (pc == null || item == null) return false;

        pc.Body = item;

        return true;
    }
    
    public async Task<bool> SetBoots(int characterId, int itemId)
    {
        var pc = await context.PlayerCharacters.Where(p => p.Id == characterId).FirstOrDefaultAsync();
        var item = await context.ItemCollection.Where(h => h.Id == itemId).FirstOrDefaultAsync();
        if (pc == null || item == null) return false;

        pc.Feet = item;

        return true;
    }


}
