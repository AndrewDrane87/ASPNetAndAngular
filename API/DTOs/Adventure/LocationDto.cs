using API.Entities.Adventure;

namespace API.DTOs.Adventure
{
    public class LocationDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<NPC> NPCs { get; set; }
        public List<ConnectedLocationDto> ConnectedLocations { get; set; }
        public List<ContainerDto> Containers { get; set; }
        public List<Interaction> Interactions { get; set; }
    }

    public class ConnectedLocationDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
