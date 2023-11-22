using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace API.Entities
{
    public class Location
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public List<NPC> NPCs { get; set; }
        public List<LocationLink> ConnectedToLocations { get; set; }
        public List<LocationLink> ConnectedFromLocations { get; set; }
        public List<Interaction> Interactions { get; set; }
        public List<Container> Containers { get; set; }
        public List<ActionTrigger> Triggers { get; set; }
        public List<EnemyLocationLink> EnemyLocationLinks { get; set; }
        public string VisibilityRequirements { get; set; }
        public int? RoomNumber { get; set; }
        public bool ItemsRequirePurchase { get; set; }
    }

    public class LocationLink
    {
        public Location FromLocation { get; set; }
        public int FromId { get; set; }
        public int ToId { get; set; }
    }

    public class EnemyLocationLink
    {
        public int Id { get; set; }

        [ForeignKey("Location")]
        public int LocationId { get; set; }
        public Location Location { get; set; }

        [ForeignKey("Enemy")]
        public int EnemyId { get; set; }
        public Enemy Enemy { get; set; }
        public int RequiredPlayerCount { get; set; }
    }

    public class Interaction
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Information { get; set; }
        public int LocationId { get; set; }
        public List<ActionTrigger> Triggers { get; set; }
    }

   

    
}
