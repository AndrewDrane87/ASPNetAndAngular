﻿namespace API.Entities.Adventure
{
    public class NPC
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Caption { get; set; }
        public  Dialogue Dialogue { get; set; } 
    }
}
