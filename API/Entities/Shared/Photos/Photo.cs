using System.ComponentModel.DataAnnotations.Schema;

namespace API;

[Table("Photos")]
public class Photo
{
    public int Id { get; set; }
    public string Url { get; set; }
    public string ObjectType { get; set; }
    public string ObjectSubType { get; set; }
    public string PublicId { get; set; }
}
