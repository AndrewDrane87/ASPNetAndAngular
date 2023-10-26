using System.Diagnostics.CodeAnalysis;

namespace API.Entities.Adventure
{
    public class Dialogue
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public List<DialogueResponseLink> ChildResponses { get; set; }
    }

    public class DialogueResponse
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DialogueResponseLink ChildDialogue { get; set; }
    }

    public class DialogueResponseLink
    {
        public int Id { get; set; }
        
        public Dialogue FromDialogue { get; set; }
        public int? FromDialogueId { get; set; }
        public DialogueResponse ToResponse { get; set; }
        public int? ToResponseId { get; set; }

        public Dialogue ToDialogue { get; set; }
        public int? ToDialogueId { get; set; }
        public DialogueResponse FromResponse { get; set; }
        public int? FromResponseId { get; set; }
    }

    public class DialogDto
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public List<DialogueResponse> Responses { get; set; }
    }
}
