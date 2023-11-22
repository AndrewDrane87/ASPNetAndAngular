using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace API.Entities
{
    public class DialogueNode
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public List<DialogueLink> ToDialogueLinks { get; set; }
        public List<ActionTrigger> Triggers { get; set; }
    }

    public class DialogueLink
    {
        [ForeignKey("FromDialogue")]
        public int FromDialogueId { get; set; }
        public DialogueNode FromDialogue { get; set; }
        [ForeignKey("ToDialogue")]
        public int ToDialogueId { get; set; }
        public DialogueNode ToDialogue { get; set; }
    }


    /*
    public class xDialogue
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public List<DialogueResponse> ChildResponses { get; set; }
        public List<xDialogueResponseLink> ParentResponses { get; set; }
        public List<ActionTrigger> Triggers { get; set; }
    }

    public class xDialogueResponseLink
    {
        public int Id { get; set; }

        [ForeignKey("Response")]
        public int? ResponseId { get; set; }
        public DialogueResponse Response { get; set; }

        [ForeignKey("ChildDialogue")]
        public int? ChildDialogueId { get; set; }
        public xDialogue ChildDialogue { get; set; }
    }

    public class DialogueResponse
    {
        public int Id { get; set; }

        public string Text { get; set; }

        [ForeignKey("ParentDialogue")]
        public int? ParentDialogueId { get; set; }
        public xDialogue ParentDialogue { get; set; }

        [ForeignKey("ChildDialogueLink")]
        public int? DialogueResponseLinkId { get; set; }
        public xDialogueResponseLink ChildDialogueLink { get; set; }
    }

    */

    
}
