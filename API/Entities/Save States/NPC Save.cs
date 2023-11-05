using API.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    public class NPCSave
    {
        public int Id { get; set; }
        [ForeignKey("NPC")]
        public int NpcId { get; set; }
        public NPC NPC { get; set; }
        public DialogueSave Dialogue { get; set; }
    }
}
