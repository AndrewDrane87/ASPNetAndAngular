namespace API.Entities.Adventure
{
    public class Location
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<NPC> NPCs { get; set; }
        public List<LocationLink> ConnectedToLocations { get; set; }
        public List<LocationLink> ConnectedFromLocations { get; set; }
        public List<Container> Containers { get; set; }
    }

    public class LocationLink
    {
        public Location FromLocation { get; set; }
        public int FromId { get; set; }
        public int ToId { get; set; }
    }
}
