namespace API.Entities
{
    public class NewContainerDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int LocationId { get; set; }
    }

    public class ContainerDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<ItemDto> Items { get; set; }
        public List<Trigger> Triggers { get; set; }

        public static ContainerDto Convert(Container container)
        {
            ContainerDto dto = new ContainerDto
            {
                Id = container.Id,
                Name = container.Name,
                Description = container.Description,
            };
            dto.Items = new List<ItemDto>();

            foreach (ItemContainerLink i in container.Items)
                dto.Items.Add(ItemDto.Convert(i.Item));

            return dto;
        }
    }

    public class ContainerSaveDto
    {
        public int Id { get; set; }
        public int ContainerId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<ItemDto> Items { get; set; }
        public List<Trigger> Triggers { get; set; }
        public bool Complete { get; set; }

        public static ContainerSaveDto Convert(ContainerSave save)
        {
            ContainerSaveDto dto = new ContainerSaveDto
            {
                Id = save.Id,
                ContainerId = save.ContainerId,
                Name = save.Container.Name,
                Description = save.Container.Description,
                Complete = save.Complete
            };
            dto.Items = new List<ItemDto>();
            if (save.Items != null)
            {
                foreach (ItemSave i in save.Items)
                    dto.Items.Add(ItemDto.Convert(i.Item));
            }
            return dto;
        }

        public static List<ContainerSaveDto> ConvertList(List<ContainerSave> saves)
        {
            List<ContainerSaveDto> list = new List<ContainerSaveDto>();
            foreach (ContainerSave save in saves)
                list.Add(Convert(save));

            return list;
        }
    }
}
