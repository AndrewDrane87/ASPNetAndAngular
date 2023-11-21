using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    public class ItemSave
    {
        public int Id { get; set; }
        
        [ForeignKey("ContainerSave")]
        public int? ContainerSaveId { get; set; }
        public ContainerSave ContainerSave { get; set; }
        
        
        [ForeignKey("LocationSave")]
        public int? LocationSaveId { get; set; }
        public LocationSave LocationSave { get; set; }

        [ForeignKey(nameof(Item))]
        public int ItemId { get; set; }
        public Item Item { get; set; }
        public int StorageIndex { get; set; }
        public int CurrentStackSize { get; set; }
    }
}
