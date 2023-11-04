using API.DTOs;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace API.Entities.Adventure
{
    public class AdventureSave
    {
        public int Id { get; set; }
        public string SaveDescription { get; set; }

        [ForeignKey("Adventure")]
        public int AdventureId { get; set; }
        public Adventure Adventure { get; set; }

        public List<PlayerCharacter> PlayerCharacters { get; set; }

        public List<LocationSave> LocationSaves { get; set; }

        public static AdventureSaveDto Convert(AdventureSave entity)
        {
            return new AdventureSaveDto
            {
                Id = entity.Id,
                SaveDescription = entity.SaveDescription,
            };
        }

    }

    public class AdventureSaveDto
    {
        public int Id { get; set; }
        public string SaveDescription { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<PlayerCharacterDto> PlayerCharacters { get; set; }
        public LocationSaveDto CurrentLocation { get; set; }
        
        public static AdventureSaveDto Convert(AdventureSave save)
        {
            var currentLocation = save.LocationSaves.FirstOrDefault(l => l.IsCurrentLocation);

            if (currentLocation == null)
                throw new Exception();
            
            return new AdventureSaveDto
            {
                Id = save.Id,
                SaveDescription = save.SaveDescription,
                Name = save.Adventure.Name,
                Description = save.Adventure.Description,
                PlayerCharacters = PlayerCharacterDto.ConvertList(save.PlayerCharacters),
                CurrentLocation = LocationSaveDto.Convert(currentLocation),
            };
        }

        public static List<AdventureSaveDto> ConvertList(List<AdventureSave> saves)
        {

            List<AdventureSaveDto> list = new List<AdventureSaveDto>();
            if (saves != null) {
                foreach (AdventureSave save in saves)
                    list.Add(Convert(save));
            }
            return list;
        }
    }

    public class NewAdventureSave
    {
        public string SaveDescription { get; set; }
        public int PlayerId { get; set; }
        public int AdventureId { get; set; }
    }

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

        /*
         * Need to be mapped
         */
        [NotMapped]
        public List<NPC> NPCs { get; set; }
        [NotMapped]
        public List<Interaction> Interactions { get; set; }
        [NotMapped]
        public List<Container> Containers { get; set; }
        [NotMapped]
        public List<ActionTrigger> Triggers { get; set; }
        [NotMapped]
        public List<Enemy> Enemies { get; set; }
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

    public class LocationSaveDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int LocationId { get; set; }
        public List<NPC> NPCs { get; set; }
        public List<ConnectedLocationDto> ConnectedLocations { get; set; }
        public List<ContainerDto> Containers { get; set; }
        public List<Interaction> Interactions { get; set; }
        public List<ActionTrigger> Triggers { get; set; }

        public static LocationSaveDto Convert(LocationSave save)
        {
            if (save.Location == null) return null;

            LocationSaveDto dto = new LocationSaveDto
            {
                Id = save.Id,
                Name = save.Location.Name,
                Description = save.Location.Description,
                LocationId = save.LocationId,
            };

            return dto;
        }

        public static LocationSaveDto Convert(LocationSave save, LocationDto locationDto)
        {
            if(save.Location == null) return null;

            LocationSaveDto dto = new LocationSaveDto
            {
                Id = save.Id,
                Name = save.Location.Name,
                Description = save.Location.Description,
                LocationId = save.LocationId,
                NPCs = locationDto.NPCs,
                ConnectedLocations = locationDto.ConnectedLocations,
                Containers = locationDto.Containers,
                Interactions = locationDto.Interactions,
                Triggers = locationDto.Triggers,
            };

            return dto;
        }

        public static List<LocationSaveDto> ConvertList(List<LocationSave> locationSaves)
        {
            List<LocationSaveDto> list = new List<LocationSaveDto>();
            if(locationSaves != null)
            {
                foreach(LocationSave save in  locationSaves)
                    list.Add(Convert(save));
            }
            return list;
        }

    }

    public class NPCSave
    {
        public int Id { get; set; }
        [ForeignKey("NPC")]
        public int NpcId { get; set; }
        public NPC NPC { get; set; }
        public DialogueSave Dialogue { get; set; }
    }

    public class DialogueSave
    {
        public int Id { get; set; }
        [ForeignKey("Dialogue")]
        public int DialogueId { get; set; }
        public Dialogue Dialogue { get; set; }
        public List<DialogueResponseSave> ChildResponses { get; set; }
        public DialogueResponseSave ParentResponse { get; set; }
        public List<ActionTriggerSave> Triggers { get; set; }
    }

    public class DialogueResponseSave
    {
        public int Id { get; set; }
        [ForeignKey("DialogueResponse")]
        public int DialogueResponseId { get; set; }
        public DialogueResponse DialogueResponse { get; set; }
    }

    public class ActionTriggerSave
    {
        public int Id { get; set; }
        [ForeignKey("ActionTrigger")]
        public int ActionTriggerId { get; set; }
        public ActionTrigger ActionTrigger { get; set; }
    }
}

