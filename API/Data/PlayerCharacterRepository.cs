
using API.Data;
using API.Entities;
using API.Helpers;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using CloudinaryDotNet.Actions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System.Diagnostics.Eventing.Reader;

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
            .Include(p => p.Helmet).ThenInclude(helmet => helmet.Item.Photo)
            .Include(p => p.LeftHand).ThenInclude(leftHand => leftHand.Item.Photo)
            .Include(p => p.RightHand).ThenInclude(rightHand => rightHand.Item.Photo)
            .Include(p => p.Body).ThenInclude(body => body.Item.Photo)
            .Include(p => p.Feet).ThenInclude(feet => feet.Item.Photo)
            .Include(p => p.BackPack).ThenInclude(i => i.Item.Photo)
            .FirstOrDefaultAsync();

        var pcDto = new PlayerCharacterDto()
        {
            Id = characterId,
            Name = pc.Name,
            PhotoUrl = pc.PhotoUrl,
            Helmet = ItemSaveDto.Convert(pc.Helmet),
            LeftHand = ItemSaveDto.Convert(pc.LeftHand),
            Body = ItemSaveDto.Convert(pc.Body),
            RightHand = ItemSaveDto.Convert(pc.RightHand),
            Feet = ItemSaveDto.Convert(pc.Feet),
            BackPack = ItemSaveDto.ConvertArray(pc.BackPack.ToArray())
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

    public async Task<StatusMessage> SetHelmet(int characterId, int itemId)
    {
        var pc = await context.PlayerCharacters.Where(p => p.Id == characterId).FirstOrDefaultAsync();
        var item = await context.ItemSaves.Where(h => h.Id == itemId).FirstOrDefaultAsync();
        if (pc == null || item == null) return new StatusMessage { Status = false };
        item.ContainerSave = null;
        item.LocationSave = null;
        pc.Helmet = item;

        return new StatusMessage { Status = true };
    }

    public async Task<StatusMessage> SetLeftHand(int characterId, int itemId)
    {
        var pc = await context.PlayerCharacters.Where(p => p.Id == characterId).FirstOrDefaultAsync();
        var item = await context.ItemSaves.Where(h => h.Id == itemId).FirstOrDefaultAsync();
        if (pc == null || item == null) return new StatusMessage { Status = false };
        item.ContainerSave = null;
        item.LocationSave = null;
        pc.LeftHand = item;

        return new StatusMessage { Status = true };
    }

    public async Task<StatusMessage> SetRightHand(int characterId, int itemId)
    {
        var pc = await context.PlayerCharacters.Where(p => p.Id == characterId).FirstOrDefaultAsync();
        var item = await context.ItemSaves.Where(h => h.Id == itemId).FirstOrDefaultAsync();
        if (pc == null || item == null) return new StatusMessage { Status = false };
        item.ContainerSave = null;
        item.LocationSave = null;
        pc.RightHand = item;

        return new StatusMessage { Status = true };
    }

    public async Task<StatusMessage> SetArmor(int characterId, int itemId)
    {
        var pc = await context.PlayerCharacters.Where(p => p.Id == characterId).FirstOrDefaultAsync();
        var item = await context.ItemSaves.Where(h => h.Id == itemId).FirstOrDefaultAsync();
        if (pc == null || item == null) return new StatusMessage { Status = false };
        item.ContainerSave = null;
        item.LocationSave = null;
        pc.Body = item;

        return new StatusMessage { Status = true };
    }



    public async Task MoveItemToLocation(int locationSaveId, ItemSave item)
    {
        LocationSave location = await context.LocationSaves
            .Include(l => l.Items)
            .FirstOrDefaultAsync(l => l.Id == locationSaveId);
        location.Items.Add(item);
    }

    public async Task<StatusMessage> SetCharacterItem(int characterId, int itemId, string slot)
    {
        var pc = await context.PlayerCharacters
            .Include(p => p.AdventureSave)
            .Include(p => p.Helmet)
            .Include(p => p.LeftHand)
            .Include(p => p.Body)
            .Include(p => p.RightHand)
            .Include(p => p.Feet)
            .Include(p => p.BackPack).ThenInclude(b => b.Item)
            .FirstOrDefaultAsync(p => p.Id == characterId);

        var newItem = await context.ItemSaves
            .Include(i => i.LocationSave)
            .Include(i => i.ContainerSave)
            .Where(h => h.Id == itemId).FirstOrDefaultAsync();

        if (pc == null || newItem == null) return new StatusMessage { Status = false };



        if (newItem.LocationSave != null)
            newItem.LocationSave.Items.Remove(newItem);
        if (newItem.ContainerSave != null)
            newItem.ContainerSave.Items.Remove(newItem);

        int storageIndex = -1;
        if (pc.BackPack.Contains(newItem))
        {
            pc.BackPack.Remove(newItem);
            storageIndex = newItem.StorageIndex;
            newItem.StorageIndex = -1;
        }

        ItemSave oldItem;
        switch (slot)
        {
            case "helmet":
                oldItem = pc.Helmet;
                //await MoveItemToLocation(pc.AdventureSave.CurrentLocationId ?? default, pc.Helmet);
                pc.Helmet = newItem; break;
            case "leftHand":
                oldItem = pc.LeftHand;
                //await MoveItemToLocation(pc.AdventureSave.CurrentLocationId ?? default, pc.LeftHand);
                pc.LeftHand = newItem; break;
            case "armor":
                oldItem = pc.Body;
                //await MoveItemToLocation(pc.AdventureSave.CurrentLocationId ?? default, pc.Body);
                pc.Body = newItem; break;
            case "rightHand":
                oldItem = pc.RightHand;
                //await MoveItemToLocation(pc.AdventureSave.CurrentLocationId ?? default, pc.RightHand); 
                pc.RightHand = newItem; break;
            case "boots":
                oldItem = pc.Feet;
                //await MoveItemToLocation(pc.AdventureSave.CurrentLocationId ?? default, pc.Feet); 
                pc.Feet = newItem; break;
            default: return new StatusMessage { Status = false, Message = "Improper item location set" };
        }

        if (storageIndex != -1)
        {
            oldItem.StorageIndex = storageIndex;
            pc.BackPack.Add(oldItem);
        }
        else
            await MoveItemToLocation(pc.AdventureSave.CurrentLocationId ?? default, oldItem);

        return new StatusMessage { Status = true };
    }

    public async Task<StatusMessage> SetBackpack(int characterId, int itemId, int backPackIndex)
    {
        var pc = await context.PlayerCharacters
            .Include(p => p.BackPack)
            .Include(p => p.AdventureSave)
            .Where(p => p.Id == characterId).FirstOrDefaultAsync();

        var item = await context.ItemSaves
            .Include(i => i.ContainerSave)
            .Include(i => i.LocationSave)
            .Include(i => i.ContainerSave)
            .Where(h => h.Id == itemId).FirstOrDefaultAsync();
        item.StorageIndex = backPackIndex;

        if (pc == null || item == null)
            return new StatusMessage { Status = false, Message = "Could not find PC or Item" };

        if (item.ContainerSave != null)
            item.ContainerSave.Items.Remove(item);

        if (item.LocationSave != null)
            item.LocationSave.Items.Remove(item);

        //Check for existing item and remove
        var existingItem = pc.BackPack.FirstOrDefault(i => i.StorageIndex == backPackIndex);
        if (existingItem != null)
        {
            if (existingItem.Id != item.Id)
            {
                pc.BackPack.Remove(existingItem);
                existingItem.StorageIndex = -1;
                existingItem.LocationSaveId = pc.AdventureSave.CurrentLocationId;
            }
        }


        pc.BackPack.Add(item);

        await context.SaveChangesAsync();

        return new StatusMessage { Status = true };
    }

    public async Task<StatusMessage> UseItem(int playerCharacterId, int ItemId)
    {
        var itemSave = await context.ItemSaves.FirstOrDefaultAsync(i => i.Id == ItemId);
        if (itemSave == null) return new StatusMessage { Status = false, Message = "Could not find item save" };

        var pc = await context.PlayerCharacters.Include(p => p.BackPack).ThenInclude(b => b.Item).FirstOrDefaultAsync(p => p.Id == playerCharacterId);

        itemSave.CurrentStackSize -= 1;
        
        if (itemSave.CurrentStackSize <= 0)
            pc.BackPack.Remove(itemSave);

        await context.SaveChangesAsync();

        return new StatusMessage { Status = true };
    }


}
