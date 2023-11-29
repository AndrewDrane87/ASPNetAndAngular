using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace API.Entities
{
    public class InteractionSave
    {
        public int Id { get; set; }
        public bool Complete { get; set; }
        public bool Passed { get; set; }

        public List<TriggerSave> TriggerSaves { get; set; }

        [ForeignKey("Interaction")]
        public int InteractionId { get; set; }
        public Interaction Interaction { get; set; }

        [ForeignKey("LocationSave")]
        public int LocationSaveId { get; set; }
        [JsonIgnore]
        public LocationSave LocationSave { get; set; }
    }
}
