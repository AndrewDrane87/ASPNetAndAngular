using API.DTOs.Items;

namespace API.Entities
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

        public static PlayerCharacterDto Convert(PlayerCharacter pc)
        {
            return new PlayerCharacterDto { Id = pc.Id, Name = pc.Name };
        }

        public static List<PlayerCharacterDto> ConvertList(List<PlayerCharacter> playerCharacters)
        {

            List<PlayerCharacterDto> list = new List<PlayerCharacterDto>();
            if (playerCharacters != null)
                foreach (PlayerCharacter pc in playerCharacters)
                    list.Add(new PlayerCharacterDto { Id = pc.Id, Name = pc.Name, PhotoUrl = pc.PhotoUrl });
            return list;
        }
    }
}
