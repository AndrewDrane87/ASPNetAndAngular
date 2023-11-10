using API.Entities;

namespace API.Helpers
{
    public static class DTOConversion
    {
        public static ItemDto ConvertItem(Item item)
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
                DamageType = item.DamageType
            };
        }

    }
}
