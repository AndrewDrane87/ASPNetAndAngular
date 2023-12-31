﻿using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace API.Entities
{
    public class LocationSave
    {
        #region Save Data
        public int Id { get; set; }
        [ForeignKey("AdventureSave")]
        public int AdventureSaveId { get; set; }
        [JsonIgnore]
        public AdventureSave AdventureSave { get; set; }

        [ForeignKey("Location")]
        public int LocationId { get; set; }
        public Location Location { get; set; }
        public bool IsCurrentLocation { get; set; }
        public List<TriggerSave> Triggers { get; set; }
        public string VisibilityRequirement { get; set; }
        public List<EnemySave> Enemies { get; set; }
        public List<ContainerSave> Containers { get; set; }
        public List<ItemSave> Items { get; set; }
        public List<InteractionSave> Interactions { get; set; }
        
        
        /*
         * Need to be mapped
         */
        [NotMapped]
        public List<NPC> NPCs { get; set; }
        
        #endregion

        #region Not Mapped
        [NotMapped]
        public string Name { get; set; }
        [NotMapped]
        public string Description { get; set; }
        [NotMapped]
        public List<LocationLink> ConnectedToLocations { get; set; }
        [NotMapped]
        public List<LocationLink> ConnectedFromLocations { get; set; }
        #endregion


    }

    public class AvailableItemLink
    {
        public int Id { get; set; }
        [ForeignKey("Item")]
        public int ItemId { get; set; }
        [ForeignKey("LocationSave")]
        public int LocationSaveId { get; set; }
        public Item Item { get; set; }
        public LocationSave LocationSave { get; set; }
    }
}
