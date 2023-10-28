﻿using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities.Adventure
{
    public class Location
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<NPC> NPCs { get; set; }
        public List<LocationLink> ConnectedToLocations { get; set; }
        public List<LocationLink> ConnectedFromLocations { get; set; }
        public List<Interaction> Interactions { get; set; }
        public List<Container> Containers { get; set; }
    }

    public class LocationLink
    {
        public Location FromLocation { get; set; }
        public int FromId { get; set; }
        public int ToId { get; set; }
    }

    public class Interaction
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Information { get; set; }
        public int LocationId { get; set; }
    }
}