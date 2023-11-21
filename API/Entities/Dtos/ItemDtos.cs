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
        public string DamageModifiers { get; set; }
        public string StatModifiers { get; set; }
        public string ResistanceModifiers { get; set; }
        public string ItemType { get; set; }
        public string DamageType { get; set; }
        public int Value { get; set; }
        public int StackSize { get; set; } = 1;
        public string Use { get; set; }

        public static ItemDto Convert(Item item)
        {
            return new ItemDto
            {
                Id = item.Id,
                Name = item.Name,
                RequiredLevel = item.RequiredLevel,
                PhotoUrl = item.Photo == null ? StaticResources.NoImageAvailableUrl :item.Photo.Url,
                AttackValue = item.AttackValue,
                ArmorValue = item.ArmorValue,
                DamageModifiers = item.DamageModifiers,
                StatModifiers = item.StatModifiers,
                ResistanceModifiers = item.ResistanceModifiers,
                ItemType = item.ItemType,
                DamageType = item.DamageType == null ? "" : item.DamageType,
                Value = item.Value,
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
        public string DamageModifiers { get; set; }
        public string StatModifiers { get; set; }
        public string ResistanceModifiers { get; set; }
        public string ItemType { get; set; }
        public string DamageType { get; set; }
        public int StorageIndex { get; set; }
        public int Value { get; set; }

        private int maxStackSize;
        public int MaxStackSize
        {
            get { return maxStackSize; }
            set
            {
                maxStackSize = value;
                CurrentStackSize = value;
            }
        }

        public int CurrentStackSize { get; set; }
        public string Use { get; set; }
        public static ItemSaveDto Convert(ItemSave save)
        {
            return new ItemSaveDto
            {
                Id = save.Id,
                ItemId = save.ItemId,
                Name = save.Item.Name,
                RequiredLevel = save.Item.RequiredLevel,
                PhotoUrl = save.Item.Photo == null ? StaticResources.NoImageAvailableUrl : save.Item.Photo.Url,
                AttackValue = save.Item.AttackValue,
                ArmorValue = save.Item.ArmorValue,
                DamageModifiers = save.Item.DamageModifiers == null ? "" : save.Item.DamageModifiers,
                StatModifiers = save.Item.StatModifiers == null ? "" : save.Item.StatModifiers,
                ResistanceModifiers = save.Item.ResistanceModifiers == null ? "" : save.Item.ResistanceModifiers,
                ItemType = save.Item.ItemType,
                DamageType = save.Item.DamageType == null ? "" : save.Item.DamageType,
                StorageIndex = save.StorageIndex,
                Value = save.Item.Value,
                MaxStackSize = save.Item.StackSize,
                CurrentStackSize = save.CurrentStackSize,
                Use = save.Item.Use,
                
            };
        }

        public static List<ItemSaveDto> ConvertList(List<ItemSave> saves)
        {
            List<ItemSaveDto> list = new List<ItemSaveDto>();
            foreach (ItemSave save in saves)
                list.Add(Convert(save));

            return list;
        }

        public static ItemSaveDto[] ConvertArray(ItemSave[] saves)
        {
            ItemSaveDto[] array = new ItemSaveDto[10];
            foreach (ItemSave save in saves)
                array[save.StorageIndex] = Convert(save);

            return array;
        }
    }
}
