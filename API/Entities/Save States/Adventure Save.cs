using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace API.Entities
{
    public class AdventureSave
    {
        public int Id { get; set; }
        public string SaveDescription { get; set; }

        [ForeignKey("Adventure")]
        public int AdventureId { get; set; }
        public Adventure Adventure { get; set; }

        public List<PlayerCharacter> PlayerCharacters { get; set; }

        [ForeignKey("CurrentLocation")]
        public int? CurrentLocationId { get; set; }
        public LocationSave CurrentLocation { get; set; }

        public List<LocationSave> LocationSaves { get; set; }
        public List<AdventureVariableSave> Variables { get; set; }
    }

    public class AdventureVariableSave
    {
        public int Id { get; set; }

        [ForeignKey("AdventureVariable")]
        public int AdventureVariableId { get; set; }
        public AdventureVariable AdventureVariable { get; set; }
        public string Value { get; set; }
        
        [ForeignKey("AdventureSave")]
        public int AdventureSaveId { get; set; }
        [JsonIgnore]
        public AdventureSave AdventureSave { get; set; }

    }

    /// <summary>
    /// Used when a user requests the creation of a new adventure save
    /// </summary>
    public class NewAdventureSave
    {
        public string SaveDescription { get; set; }
        public int PlayerId { get; set; }
        public int AdventureId { get; set; }
    }
}
