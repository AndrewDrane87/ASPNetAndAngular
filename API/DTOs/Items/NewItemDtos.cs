namespace API.DTOs.Items
{
    public class NewItemDto
    {
        public string Name { get; set; }
        public int PhotoId { get; set; }
        public int AttackValue { get; set; }
        public int ArmorValue { get; set; }
        public int RequiredLevel { get;set; }
        public string Modifiers { get; set; }
        public string ItemType { get; set; }

    }
}
