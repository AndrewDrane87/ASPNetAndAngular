namespace API.Entities.Adventure
{
    public class Container
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<ContainerItem> Items { get; set; }
        public List<ActionTrigger> Triggers { get; set; }
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
        public List<ActionTrigger> Triggers { get; set; }

        public static ContainerDto Convert(Container container)
        {
            ContainerDto dto = new ContainerDto
            {
                Id = container.Id,
                Name = container.Name,
                Description = container.Description,
            };
            dto.Items = new List<ItemDto>();
            
            foreach(ContainerItem i in container.Items)
                dto.Items.Add(ItemDto.Convert(i.Item));

            return dto;
        }
    }
}
