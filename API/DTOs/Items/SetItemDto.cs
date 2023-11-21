namespace API.DTOs.Items
{
    public class SetItemDto
    {
        public string ItemType { get; set; }
        public int ItemId { get; set; }
        public int CharacterId { get; set; }
        public int BackpackIndex { get; set; }
    }
}
