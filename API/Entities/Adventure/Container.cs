namespace API.Entities.Adventure
{
    public class Container
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<ContainerItem> Items { get; set; }
    }

    public class ContainerItem
    {
        public int Id { get; set; }
        public int ContainerId { get; set; }
        public Container Container { get; set; }
        public int ItemId { get; set; }
        public Item Item { get; set; }
        
    }

    public class ContainerDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<ItemDto> Items { get; set; }
    }
}
