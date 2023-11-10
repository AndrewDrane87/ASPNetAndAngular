namespace API.Entities
{
    public class Enemy
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? PhotoId { get; set; }
        public Photo Photo { get; set; }
        public int MaxHp { get; set; }
        public int ArmorValue { get; set; }
        public int MovementRange { get; set; }
        public string AttackStrategy { get; set; }
        public string Attack1Name { get; set; }
        public int Attack1Range { get; set; }
        public int Attack1BaseDamage { get; set; }
        public string Attack2Name { get; set; }
        public int Attack2Range { get; set; }
        public int Attack2BaseDamage { get; set; }
        public int ModifierDiceSides { get; set; } = 4;
    }
}
