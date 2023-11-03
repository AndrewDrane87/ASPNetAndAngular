using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace API.Entities.Adventure
{
    public class Dialogue
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public List<DialogueResponse> ChildResponses { get; set; }
        public DialogueResponse ParentResponse { get; set; }
        public List<ActionTrigger> Triggers { get; set; }
    }

    public class DialogueResponse
    {
        public int Id { get; set; }
        public string Text { get; set; }
    }

    public class DialogueDto
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public List<DialogueResponse> Responses { get; set; }
        public List<ActionTrigger> Triggers { get; set; }

        public static DialogueDto Convert(Dialogue d)
        {
            return new DialogueDto { Id = d.Id, Text = d.Text, Responses = d.ChildResponses };
        }
    }
}
