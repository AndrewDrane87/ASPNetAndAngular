using API.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    public class DialogueSave
    {
        public int Id { get; set; }
        [ForeignKey("Dialogue")]
        public int DialogueId { get; set; }
        public Dialogue Dialogue { get; set; }
        public List<DialogueResponseSave> ChildResponses { get; set; }
        public DialogueResponseSave ParentResponse { get; set; }
        public List<ActionTriggerSave> Triggers { get; set; }
    }

    public class DialogueResponseSave
    {
        public int Id { get; set; }
        [ForeignKey("DialogueResponse")]
        public int DialogueResponseId { get; set; }
        public DialogueResponse DialogueResponse { get; set; }
    }
}
