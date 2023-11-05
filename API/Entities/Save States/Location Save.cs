using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace API.Entities
{
    public class LocationSave
    {
        #region Save Data
        public int Id { get; set; }
        [ForeignKey("AdventureSave")]
        public int AdventureSaveId { get; set; }
        [JsonIgnore]
        public AdventureSave AdventureSave { get; set; }

        [ForeignKey("Location")]
        public int LocationId { get; set; }
        public Location Location { get; set; }
        public bool IsCurrentLocation { get; set; }
        public List<ActionTriggerSave> Triggers { get; set; }

        /*
         * Need to be mapped
         */
        [NotMapped]
        public List<NPC> NPCs { get; set; }
        [NotMapped]
        public List<Interaction> Interactions { get; set; }
        [NotMapped]
        public List<Container> Containers { get; set; }
        
        [NotMapped]
        public List<Enemy> Enemies { get; set; }
        #endregion

        #region Not Mapped
        [NotMapped]
        public string Name { get; set; }
        [NotMapped]
        public string Description { get; set; }
        [NotMapped]
        public List<LocationLink> ConnectedToLocations { get; set; }
        [NotMapped]
        public List<LocationLink> ConnectedFromLocations { get; set; }
        #endregion


    }
}
