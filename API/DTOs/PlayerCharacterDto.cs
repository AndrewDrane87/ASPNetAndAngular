using API.DTOs.Items;

namespace API.DTOs
{
    public class PlayerCharacterDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PhotoUrl { get; set; }
#nullable enable
        public ItemDto? Helmet { get; set; }
#nullable enable
        public ItemDto? LeftHand { get; set; }
#nullable enable
        public ItemDto? RightHand { get; set; }
#nullable enable
        public ItemDto? Body { get; set; }
#nullable enable
        public ItemDto? Feet { get; set; }
        //public ItemBase[] BackPack { get; set; } = new ItemBase[10];
    }
}
