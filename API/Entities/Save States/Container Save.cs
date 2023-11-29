using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    public class ContainerSave
    {
        public int Id { get; set; }
        public bool Complete { get; set; }
        public List<ItemSave> Items { get; set; }
        public List<TriggerSave> TriggerSaves { get; set; }
        [ForeignKey("Container")]
        public int ContainerId { get; set; }
        public Container Container { get; set; }

        [ForeignKey("LocationSave")]
        public int LocationSaveId { get; set; }
        public LocationSave LocationSave { get; set; }
    }
}
