using API.Data;
using API.DTOs.Items;
using API.Entities;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace API;

public class ItemRepository
{
    private readonly DataContext context;
    private readonly IMapper mapper;

    public ItemRepository(DataContext context, IMapper mapper)
    {
        this.context = context;
        this.mapper = mapper;
    }

    public async Task<Item> CreateItem(NewItemDto dto)
    {
        ItemPhoto photo = await context.ItemPhotoCollection.Where(p => p.Id == dto.PhotoId).FirstOrDefaultAsync();

        Item newItem = new Item
        {
            Name = dto.Name,
            Photo = photo,
            AttackValue = dto.AttackValue,
            ArmorValue = dto.ArmorValue,
            StatModifiers = dto.Modifiers,
            DamageModifiers = dto.Modifiers,
            ResistanceModifiers = dto.Modifiers,
            ItemType = dto.ItemType.ToLower()
        };

        await context.ItemCollection.AddAsync(newItem);
        return newItem;
    }

    public async Task<List<ItemDto>> GetItems(string itemType = "any")
    {
        List<ItemDto> items = new List<ItemDto>();
        if (itemType == "any")
        {
            foreach(Item i in await context.ItemCollection.Include(p=>p.Photo).OrderBy(i => i.ItemType).ToListAsync())
                items.Add(ItemDto.Convert(i));
            return items;
        }

        if(itemType == "hand")
        {
            foreach (Item i in await context.ItemCollection.Where(i => i.ItemType == "sword" || i.ItemType == "shield").Include(p => p.Photo).OrderBy(i => i.ItemType).ToListAsync())
                items.Add(ItemDto.Convert(i));
        }


        foreach (Item i in await context.ItemCollection.Where(i => i.ItemType == itemType).Include(p => p.Photo).OrderBy(i => i.ItemType).ToListAsync())
            items.Add(ItemDto.Convert(i));

        return items;
    }

    public async Task<bool> DeleteItem(int itemId)
    {
        Item i = await context.ItemCollection.Where(i => i.Id == itemId).FirstOrDefaultAsync();
        if (i == null)
            return false;

        context.ItemCollection.Remove(i);
        return true;
    }

    public async Task<BasicItemCollection> GetBasicItems()
    {
        BasicItemCollection items = new BasicItemCollection();
        
        items.Helmet = await context.ItemCollection.Where(i => i.ItemType == "helmet" && i.ArmorValue == 1).Include(p => p.Photo).FirstOrDefaultAsync();
        items.Sword = await context.ItemCollection.Where(i => i.ItemType == "sword" && i.AttackValue == 1).Include(p => p.Photo).FirstOrDefaultAsync();
        items.Shield = await context.ItemCollection.Where(i => i.ItemType == "shield" && i.ArmorValue == 1).Include(p => p.Photo).FirstOrDefaultAsync();
        items.Armor = await context.ItemCollection.Where(i => i.ItemType == "armor" && i.ArmorValue == 1).Include(p => p.Photo).FirstOrDefaultAsync();
        items.Boots = await context.ItemCollection.Where(i => i.ItemType == "boot" && i.ArmorValue == 1).Include(p => p.Photo).FirstOrDefaultAsync();

        return items;
    }
}


public class BasicItemCollection
{
    public Item Helmet { get; set; }
    public Item Armor { get; set; }
    public Item Sword { get; set; }
    public Item Shield { get; set; }
    public Item Boots { get; set; }
}
