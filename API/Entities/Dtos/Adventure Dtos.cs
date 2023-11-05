namespace API.Entities
{
    public class AdventureSaveDto
    {
        public int Id { get; set; }
        public string SaveDescription { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<PlayerCharacterDto> PlayerCharacters { get; set; }
        public LocationSaveDto CurrentLocation { get; set; }

        public static AdventureSaveDto Convert(AdventureSave save)
        {
            var currentLocation = save.LocationSaves.FirstOrDefault(l => l.IsCurrentLocation);

            if (currentLocation == null)
                throw new Exception();

            return new AdventureSaveDto
            {
                Id = save.Id,
                SaveDescription = save.SaveDescription,
                Name = save.Adventure.Name,
                Description = save.Adventure.Description,
                PlayerCharacters = PlayerCharacterDto.ConvertList(save.PlayerCharacters),
                CurrentLocation = LocationSaveDto.Convert(currentLocation),
            };
        }

        public static List<AdventureSaveDto> ConvertList(List<AdventureSave> saves)
        {

            List<AdventureSaveDto> list = new List<AdventureSaveDto>();
            if (saves != null)
            {
                foreach (AdventureSave save in saves)
                    list.Add(Convert(save));
            }
            return list;
        }
    }
}
