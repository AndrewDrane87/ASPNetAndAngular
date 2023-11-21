using API.DTOs.Items;

namespace API.Entities
{
    public class PlayerCharacterDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PhotoUrl { get; set; }
#nullable enable
        public ItemSaveDto? Helmet { get; set; }
#nullable enable
        public ItemSaveDto? LeftHand { get; set; }
#nullable enable
        public ItemSaveDto? RightHand { get; set; }
#nullable enable
        public ItemSaveDto? Body { get; set; }
#nullable enable
        public ItemSaveDto? Feet { get; set; }
        public ItemSaveDto[] BackPack { get; set; } = new ItemSaveDto[0];

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
