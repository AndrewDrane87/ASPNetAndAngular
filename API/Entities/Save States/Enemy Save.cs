using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    public class EnemySave
    {
        public int Id { get; set; }
        [ForeignKey("Enemy")]
        public int EnemyId { get; set; }
        public Enemy Enemy { get; set; }
        public int CurrentHp { get; set; }
    }
}
