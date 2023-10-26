using System.ComponentModel.DataAnnotations.Schema;

namespace API;

[Table("ItemPhotos")]
public class ItemPhoto
{
    public int Id { get; set; }
    public string Url { get; set; }
    public string ItemType { get; set; }
    public string PublicId { get; set; }
}
