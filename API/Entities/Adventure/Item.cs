using API.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities;

public class Item
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int RequiredLevel { get; set; }
    public API.Photo Photo { get; set; }
    public int AttackValue { get; set; }
    public int ArmorValue { get; set; }
    public string DamageModifiers { get; set; }
    public string StatModifiers { get; set; }
    public string ResistanceModifiers { get; set; }
    public string ItemType { get; set; }
    public string DamageType { get; set; }
    public int Value { get; set; }
    public int StackSize { get; set; } = 1;
    public string Use { get; set; }
}

public class ItemContainerLink
{
    public int Id { get; set; }

    [ForeignKey(nameof(Item))]
    public int ItemId { get; set; }
    public Item Item { get; set; }

    [ForeignKey(nameof(Container))]
    public  int ContainerId { get; set; }
    public Container Container { get; set; }
}



/*
sword
shield
helmet
armor
boots

 
 
 
 
 
 
 
 */