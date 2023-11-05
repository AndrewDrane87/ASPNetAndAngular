using System.ComponentModel.DataAnnotations.Schema;

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

        public List<LocationSave> LocationSaves { get; set; }

        public static AdventureSaveDto Convert(AdventureSave entity)
        {
            return new AdventureSaveDto
            {
                Id = entity.Id,
                SaveDescription = entity.SaveDescription,
            };
        }

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
