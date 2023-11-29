namespace API.Entities
{
    public class Interaction
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string DefaultText { get; set; }
        public string FailedText { get; set; }
        public string PassedText { get; set; }
        public int LocationId { get; set; }
        public List<Trigger> Triggers { get; set; }
    }
}
