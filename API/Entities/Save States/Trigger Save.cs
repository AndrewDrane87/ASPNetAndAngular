using API.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace API.Entities
{
    public class TriggerSave
    {
        public int Id { get; set; }
        [ForeignKey("ActionTrigger")]
        public int ActionTriggerId { get; set; }
        public Trigger ActionTrigger { get; set; }

        [ForeignKey("LocationSave")]
        public int? LocationId { get; set; }
        public LocationSave LocationSave { get; set; }

        [ForeignKey("InteractionSave")]
        public int? InteractionId { get; set; }
        [JsonIgnore]
        public InteractionSave InteractionSave { get; set; }

        public bool Complete { get; set; }
        public string? Result { get; set; }
    }
}
