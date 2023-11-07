using API.Entities;
using API.Entities.Dtos;
using System.Text.Json.Serialization;

namespace API.Entities
{
    public class LocationDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        [JsonIgnore]
        public Location Location { get; set; }
        public List<NPC> NPCs { get; set; }
        public List<ConnectedLocationDto> ConnectedLocations { get; set; }
        public List<ContainerDto> Containers { get; set; }
        public List<Interaction> Interactions { get; set; }
        public List<ActionTrigger> Triggers { get; set; }
        public string VisibilityRequirements { get; set; }
    }

    public class NewLocationDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
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
        public List<ActionTriggerSaveDto> Triggers { get; set; }
        public List<EnemySaveDto> Enemies { get; set; }

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
            if (save.Location == null) return null;

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
                Triggers = ActionTriggerSaveDto.CreateList(save.Triggers),
                Enemies = EnemySaveDto.ConvertList(save.Enemies)
            };

            return dto;
        }

        public static List<LocationSaveDto> ConvertList(List<LocationSave> locationSaves)
        {
            List<LocationSaveDto> list = new List<LocationSaveDto>();
            if (locationSaves != null)
            {
                foreach (LocationSave save in locationSaves)
                    list.Add(Convert(save));
            }
            return list;
        }

    }

    public class ConnectedLocationDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string VisibilityRequirements { get; set; }
    }
}
