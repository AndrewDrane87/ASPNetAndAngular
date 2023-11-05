using API.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace API;

public class Item
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int RequiredLevel { get; set; }
    public ItemPhoto Photo { get; set; }
    public int AttackValue { get; set; }
    public int ArmorValue { get; set; }
    public string Modifiers { get; set; }
    public string ItemType { get; set; }
    public string DamageType { get; set; }

}

public class ItemDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int RequiredLevel { get; set; }
    public string PhotoUrl { get; set; }
    public int AttackValue { get; set; }
    public int ArmorValue { get; set; }
    public string Modifiers { get; set; }
    public string ItemType { get; set; }
    public string DamageType { get; set; }

    public static ItemDto Convert(Item item)
    {
        return new ItemDto
        {
            Id = item.Id,
            Name = item.Name,
            RequiredLevel = item.RequiredLevel,
            PhotoUrl = item.Photo.Url,
            AttackValue = item.AttackValue,
            ArmorValue = item.ArmorValue,
            Modifiers = item.Modifiers,
            ItemType = item.ItemType,
            DamageType = item.DamageType == null ? "": item.DamageType,
        };
    }
}


/*
sword
shield
helmet
armor
boots

 
 
 
 
 
 
 
 */