namespace API.Entities
{
    public class NPC
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Caption { get; set; }
        public  DialogueNode Dialogue { get; set; } 
    }

    public class NpcDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Caption { get; set; }
        public DialogueNodeDto Dialogue { get; set; }
    }
}
