namespace API.Entities
{
    public class Container
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<ItemContainerLink> Items { get; set; }
        public List<Trigger> Triggers { get; set; }
        public bool IsCorpse { get; set; }
    }
}
