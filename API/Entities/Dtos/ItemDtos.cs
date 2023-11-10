namespace API.Entities
{
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
                DamageType = item.DamageType == null ? "" : item.DamageType,
            };
        }
    }

    public class ItemSaveDto
    {
        public int Id { get; set; }
        public int ItemId { get; set; }
        public string Name { get; set; }
        public int RequiredLevel { get; set; }
        public string PhotoUrl { get; set; }
        public int AttackValue { get; set; }
        public int ArmorValue { get; set; }
        public string Modifiers { get; set; }
        public string ItemType { get; set; }
        public string DamageType { get; set; }

        public static ItemSaveDto Convert(ItemSave save)
        {
            return new ItemSaveDto
            {
                Id = save.Id,
                ItemId = save.ItemId,
                Name = save.Item.Name,
                RequiredLevel = save.Item.RequiredLevel,
                PhotoUrl = save.Item.Photo.Url,
                AttackValue = save.Item.AttackValue,
                ArmorValue = save.Item.ArmorValue,
                Modifiers = save.Item.Modifiers,
                ItemType = save.Item.ItemType,
                DamageType = save.Item.DamageType == null ? "" : save.Item.DamageType,
            };
        }

        public static List<ItemSaveDto> ConvertList(List<ItemSave> saves)
        {
            List<ItemSaveDto> list = new List<ItemSaveDto>();
            foreach(ItemSave save in saves)
                list.Add(Convert(save));

            return list;
        }
    }
}
