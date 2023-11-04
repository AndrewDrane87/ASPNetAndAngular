using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;
using API.Entities;
using API.Entities.Adventure;

namespace API;


public class PlayerCharacter
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string PhotoUrl { get; set; }
#nullable enable 
    public Item? Helmet { get; set; }
#nullable enable
    public Item? LeftHand { get; set; }
#nullable enable
    public Item? RightHand { get; set; }
#nullable enable
    public Item? Body { get; set; }
#nullable enable
    public Item? Feet { get; set; }
    //public ItemBase[] BackPack { get; set; } = new ItemBase[10];

    [ForeignKey("AdventureSave")]
    public int? AdventureSaveId { get; set; }
    [JsonIgnore]
    public AdventureSave? AdventureSave { get; set; }
}
