namespace API.Entities.Adventure
{
    public class Enemy
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? PhotoId { get; set; }
        public Photo Photo { get; set; }
        public int MaxHp { get; set; }
        public int ArmorValue { get; set; }
        public int? MeleeAttackId { get; set; }
        public Attack MeleeAttack { get; set; }
        public int? RangedAttackId { get; set; }
        public Attack RangedAttack { get; set; }
    }

    public class Attack
    {
        public int Id { get; set; }
        public int Range { get; set; }
        public int BaseDamage { get; set; }
    }

    public class EnemyDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PhotoUrl { get; set; }
        public int MaxHp { get; set; }
        public int ArmorValue { get; set; }
        public Attack MeleeAttack { get; set; }
        public Attack RangedAttack { get; set; }

        public static EnemyDto Convert(Enemy enemy)
        {
            return new EnemyDto
            {
                Id = enemy.Id,
                Name = enemy.Name,
                PhotoUrl = enemy.Photo == null? "" : enemy.Photo.Url,
                MaxHp = enemy.MaxHp,
                ArmorValue = enemy.ArmorValue,
                MeleeAttack = enemy.MeleeAttack,
                RangedAttack = enemy.RangedAttack,
            };
        }
    }
}
