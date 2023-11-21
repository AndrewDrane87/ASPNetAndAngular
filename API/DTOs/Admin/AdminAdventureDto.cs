using API.Entities;

namespace API.DTOs.Admin
{
    public class AdminAdventureDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<Location> Locations { get; set; }

        public static AdminAdventureDto Convert(Adventure a)
        {
            AdminAdventureDto dto = new AdminAdventureDto();
            dto.Id = a.Id;
            dto.Name = a.Name;  
            dto.Description = a.Description;
            dto.Locations = a.Locations;

            return dto;
        }

        public static List<AdminAdventureDto> ConvertList(List<Adventure> adventures)
        {
            List<AdminAdventureDto> list = new List<AdminAdventureDto>();
            foreach(Adventure ad in adventures)
            {
                list.Add(Convert(ad));
            }
            return list;

        }

       
    }
}
