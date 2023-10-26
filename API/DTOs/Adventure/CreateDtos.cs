namespace API.DTOs.Adventure
{
    public class NewLocationDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class NewNpcDto
    {
        public string Name { get; set; }
        public string Caption { get; set; }
        public int LocationId { get; set; }
    }

    public class NewContainerDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int LocationId { get; set; }
    }
}
