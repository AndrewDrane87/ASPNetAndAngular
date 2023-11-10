using System;

namespace API.Entities
{
    public class EnemyDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PhotoUrl { get; set; }
        public int MaxHp { get; set; }
        public int ArmorValue { get; set; }
        public string Attack1Name { get; set; }
        public int Attack1Range { get; set; }
        public int Attack1BaseDamage { get; set; }
        public string Attack2Name { get; set; }
        public int Attack2Range { get; set; }
        public int Attack2BaseDamage { get; set; }
        public int ModifierDiceSides { get; set; }
        public static EnemyDto Convert(Enemy enemy)
        {
            return new EnemyDto
            {
                Id = enemy.Id,
                Name = enemy.Name,
                PhotoUrl = enemy.Photo == null ? "" : enemy.Photo.Url,
                MaxHp = enemy.MaxHp,
                ArmorValue = enemy.ArmorValue,
                Attack1Name = enemy.Attack1Name,
                Attack1Range = enemy.Attack1Range,
                Attack1BaseDamage = enemy.Attack1BaseDamage,
                Attack2Name = enemy.Attack2Name,
                Attack2Range = enemy.Attack2Range,
                Attack2BaseDamage = enemy.Attack2BaseDamage,
                ModifierDiceSides = enemy.ModifierDiceSides
            };
        }
    }

    public class EnemySaveDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PhotoUrl { get; set; }
        public int CurrentHp { get; set; }
        public int MaxHp { get; set; }
        public int MovementRange { get; set; }
        public string AttackStrategy { get; set; }
        public int ArmorValue { get; set; }
        public string Attack1Name { get; set; }
        public int Attack1Range { get; set; }
        public int Attack1BaseDamage { get; set; }
        public string Attack2Name { get; set; }
        public int Attack2Range { get; set; }
        public int Attack2BaseDamage { get; set; }
        public int ModifierDiceSides { get; set; } = 4;
        public static EnemySaveDto Convert(EnemySave save)
        {
            return new EnemySaveDto
            {
                Id = save.Id,
                Name = save.Enemy.Name,
                PhotoUrl = save.Enemy.Photo == null ? "" : save.Enemy.Photo.Url,
                MaxHp = save.Enemy.MaxHp,
                CurrentHp = save.CurrentHp,
                MovementRange = save.Enemy.MovementRange,
                AttackStrategy = save.Enemy.AttackStrategy,
                ArmorValue = save.Enemy.ArmorValue,
                Attack1Name = save.Enemy.Attack1Name,
                Attack1Range = save.Enemy.Attack1Range,
                Attack1BaseDamage = save.Enemy.Attack1BaseDamage,
                Attack2Name = save.Enemy.Attack2Name,
                Attack2Range = save.Enemy.Attack2Range,
                Attack2BaseDamage = save.Enemy.Attack2BaseDamage,
                ModifierDiceSides = save.Enemy.ModifierDiceSides
            };
        }

        public static List<EnemySaveDto> ConvertList(List<EnemySave> saves)
        {
            List<EnemySaveDto> list = new List<EnemySaveDto>();

            foreach (EnemySave save in saves)
                list.Add(Convert(save));

            return list;
        }
    }
}
