using API.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    public class ActionTriggerSave
    {
        public int Id { get; set; }
        [ForeignKey("ActionTrigger")]
        public int ActionTriggerId { get; set; }
        public ActionTrigger ActionTrigger { get; set; }

        [ForeignKey("LocationSave")]
        public int LocationId { get; set; }
        public LocationSave LocationSave { get; set; }

        public bool Complete { get; set; }
        public string? Result { get; set; }
    }
}
