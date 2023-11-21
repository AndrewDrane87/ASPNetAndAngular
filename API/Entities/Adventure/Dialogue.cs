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


    public class Dialogue
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public List<DialogueResponse> ChildResponses { get; set; }
        public List<DialogueResponseLink> ParentResponses { get; set; }
        public List<ActionTrigger> Triggers { get; set; }
    }

    public class DialogueResponseLink
    {
        public int Id { get; set; }

        [ForeignKey("Response")]
        public int? ResponseId { get; set; }
        public DialogueResponse Response { get; set; }

        [ForeignKey("ChildDialogue")]
        public int? ChildDialogueId { get; set; }
        public Dialogue ChildDialogue { get; set; }
    }

    public class DialogueResponse
    {
        public int Id { get; set; }

        public string Text { get; set; }

        [ForeignKey("ParentDialogue")]
        public int? ParentDialogueId { get; set; }
        public Dialogue ParentDialogue { get; set; }

        [ForeignKey("ChildDialogueLink")]
        public int? DialogueResponseLinkId { get; set; }
        public DialogueResponseLink ChildDialogueLink { get; set; }
    }

    public class DialogueDto
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public List<DialogueResponseDto> Responses { get; set; }
        public List<ActionTrigger> Triggers { get; set; }

        public static DialogueDto Convert(Dialogue d)
        {
            DialogueDto dto = new DialogueDto { Id = d.Id, Text = d.Text, Responses = DialogueResponseDto.ConvertList(d.ChildResponses) };
            return dto;
        }
    }

    public class DialogueResponseDto
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public int ChildDialogueId { get; set; }

        public static DialogueResponseDto Convert(DialogueResponse response)
        {
            int childDialogueId = response.ChildDialogueLink == null ? -1 : (int)response.ChildDialogueLink.ChildDialogueId;
            DialogueResponseDto dto = new DialogueResponseDto { Id = response.Id, Text = response.Text, ChildDialogueId = childDialogueId };
            return dto;
        }

        public static List<DialogueResponseDto> ConvertList(List<DialogueResponse> responses)
        {
            List<DialogueResponseDto> List = new List<DialogueResponseDto>();
            foreach (DialogueResponse response in responses)
                List.Add(Convert(response));
            return List;

        }
    }
}
