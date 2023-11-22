namespace API.Entities
{
    public class DialogueNodeDto
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public List<DialogueNodeDto> Responses { get; set; }
        public List<ActionTrigger> Triggers { get; set; }

        public static DialogueNodeDto Convert(DialogueNode d)
        {
            List<DialogueNodeDto> responses = null;
            if (d.ToDialogueLinks != null)
            {
                responses = new List<DialogueNodeDto>();
                foreach (DialogueLink link in d.ToDialogueLinks)
                    responses.Add(Convert(link.ToDialogue));
            }

            DialogueNodeDto dto = new DialogueNodeDto { Id = d.Id, Text = d.Text, Responses = responses };
            return dto;
        }
    }

    /*
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
    */
}
