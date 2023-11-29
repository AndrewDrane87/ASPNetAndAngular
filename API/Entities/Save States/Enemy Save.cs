using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace API.Entities
{
    public class EnemySave
    {
        public int Id { get; set; }

        [ForeignKey("EnemyLocationLink")]
        public int EnemyLocationLinkId { get; set; }
        public EnemyLocationLink EnemyLocationLink { get; set; }

        [ForeignKey("Enemy")]
        public int EnemyId { get; set; }
        public Enemy Enemy { get; set; }

        [ForeignKey("LocationSave")]
        public int LocationSaveId { get; set; }
        [JsonIgnore]
        public LocationSave LocationSave { get; set; }


        public int CurrentHp { get; set; }
    }
}
