using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace API.Entities;

public class PlayerCharacter
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string PhotoUrl { get; set; }
#nullable enable

    [ForeignKey("Helmet")]
    public int? HelmetItemSaveId { get; set; }
    public ItemSave Helmet { get; set; }

    [ForeignKey("LeftHand")]
    public int? LeftHandItemSaveId { get; set; }
    public ItemSave LeftHand { get; set; }

    [ForeignKey("RightHand")]
    public int? RightHandItemSaveId { get; set; }
    public ItemSave RightHand { get; set; }

    [ForeignKey("Body")]
    public int? BodyItemSaveId { get; set; }
    public ItemSave Body { get; set; }

    [ForeignKey("Feet")]
    public int? FeetItemSaveId { get; set; }
    public ItemSave Feet { get; set; }

    public List<ItemSave> BackPack { get; set; }

    [ForeignKey("AdventureSave")]
    public int? AdventureSaveId { get; set; }
    [JsonIgnore]
    public AdventureSave? AdventureSave { get; set; }
}
