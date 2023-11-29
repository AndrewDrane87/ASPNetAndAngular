using API.Entities;
using System.Text.Json.Serialization;

namespace API.Entities
{
    public class LocationDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }

        [JsonIgnore]
        public Location Location { get; set; }
        public List<NPC> NPCs { get; set; }
        public List<ConnectedLocationDto> ConnectedLocations { get; set; }
        public List<ContainerDto> Containers { get; set; }
        public List<Interaction> Interactions { get; set; }
        public List<Trigger> Triggers { get; set; }
        public string VisibilityRequirements { get; set; }
        public int RoomNumber { get; set; }
        public bool ItemsRequirePurchase { get; set; }

        public static LocationDto Convert(Location location, List<ConnectedLocationDto> connectedLocations, List<ContainerDto> containers)
        {
            return new LocationDto
            {
                Id = location.Id,
                Name = location.Name,
                ShortDescription = location.ShortDescription,
                Description = location.Description,
                ConnectedLocations = connectedLocations,
                NPCs = location.NPCs,
                Containers = containers,
                Interactions = location.Interactions,
                Triggers = location.Triggers,
                Location = location,
                VisibilityRequirements = location.VisibilityRequirements,
                RoomNumber = location.RoomNumber == null ? -1 : (int)location.RoomNumber,
                ItemsRequirePurchase = location.ItemsRequirePurchase,
            };
        }
    }

    public class NewLocationDto
    {
        public string Name { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public int RoomNumber { get; set; } = -1;
    }

    public class LocationSaveDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public int LocationId { get; set; }
        public List<NPC> NPCs { get; set; }
        public List<ConnectedLocationDto> ConnectedLocations { get; set; }
        public List<ContainerSaveDto> Containers { get; set; }
        public List<InteractionSaveDto> Interactions { get; set; }
        public List<TriggerSaveDto> Triggers { get; set; }
        public List<EnemySaveDto> Enemies { get; set; }
        public List<Item> AvailableItems { get; set; }
        public int RoomNumber { get; set; }
        public bool ItemsRequirePurchase { get; set; }


        public static LocationSaveDto Convert(LocationSave save)
        {
            if (save.Location == null) return null;

            LocationSaveDto dto = new LocationSaveDto
            {
                Id = save.Id,
                Name = save.Location.Name,
                ShortDescription = save.Location.ShortDescription,
                Description = save.Location.Description,
                LocationId = save.LocationId,
                ItemsRequirePurchase = save.Location.ItemsRequirePurchase
            };

            return dto;
        }

        public static LocationSaveDto Convert(LocationSave save, LocationDto locationDto)
        {
            if (save.Location == null) return null;

            List<EnemySave> livingEnemies = new List<EnemySave>();
            foreach (EnemySave e in save.Enemies)
            {
                if (e.CurrentHp > 0)
                    livingEnemies.Add(e);
            }

            LocationSaveDto dto = new LocationSaveDto
            {
                Id = save.Id,
                Name = save.Location.Name,
                ShortDescription = save.Location.ShortDescription,
                Description = save.Location.Description,
                LocationId = save.LocationId,
                NPCs = locationDto.NPCs,
                ConnectedLocations = locationDto.ConnectedLocations,
                Containers = ContainerSaveDto.ConvertList(save.Containers),
                Interactions = InteractionSaveDto.ConvertList(save.Interactions),
                Triggers = TriggerSaveDto.ConvertList(save.Triggers),
                Enemies = EnemySaveDto.ConvertList(livingEnemies),
                RoomNumber = locationDto.RoomNumber,
                ItemsRequirePurchase = save.Location.ItemsRequirePurchase
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
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public string VisibilityRequirements { get; set; }
        public int RoomNumber { get; set; }

        public static ConnectedLocationDto Convert(Location location)
        {
            ConnectedLocationDto dto = new ConnectedLocationDto
            {
                Id = location.Id,
                Name = location.Name,
                ShortDescription = location.ShortDescription,
                Description = location.Description,
                VisibilityRequirements = location.VisibilityRequirements,
                RoomNumber = location.RoomNumber == null ? -1 : (int)location.RoomNumber,
            };


            return dto;
        }
    }
}
