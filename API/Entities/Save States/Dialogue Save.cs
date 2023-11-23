using API.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    public class DialogueSave
    {
        public int Id { get; set; }

        [ForeignKey("DialogueNodeDto")]
        public int DialogueNodeId { get; set; }
        public DialogueNode DialogueNode { get; set; }
        public List<TriggerSave> Triggers { get; set; }
    }

    /*
    public class DialogueResponseSave
    {
        public int Id { get; set; }
        [ForeignKey("DialogueResponse")]
        public int DialogueResponseId { get; set; }
        public DialogueResponse DialogueResponse { get; set; }
    }
    */
}
