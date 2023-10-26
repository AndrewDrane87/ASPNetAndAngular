using API.Entities.Adventure;

namespace API.DTOs.Admin
{
    public class AdminAdventureDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<Location> Locations { get; set; }
    }
}
