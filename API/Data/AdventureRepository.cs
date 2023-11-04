using API.DTOs.Admin;
using API.DTOs.Adventure;
using API.Entities.Adventure;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using System.Diagnostics.Eventing.Reader;

namespace API.Data
{
    public class AdventureRepository
    {
        private readonly DataContext context;
        private readonly IMapper mapper;

        public AdventureRepository(DataContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        #region Adventure Crud
        public async Task<Adventure> CreateAdventure(Adventure adventure)
        {
            await context.Adventures.AddAsync(adventure);
            return adventure;
        }



        public async Task<List<AdventureSaveDto>> GetAvailableAdventures(int userId)
        {
            var user = await context.Users
                .Include(u => u.AdventureSaves).ThenInclude(a => a.Adventure)
                .Include(u => u.AdventureSaves).ThenInclude(a => a.PlayerCharacters)
                .Include(u => u.AdventureSaves).ThenInclude(a => a.LocationSaves).ThenInclude(ls => ls.Location)
                .FirstOrDefaultAsync(u => u.Id == userId);

            return AdventureSaveDto.ConvertList(user.AdventureSaves);
        }

        public async Task<Adventure> GetAdventureForAdmin(int id)
        {
            var adventure = await context.Adventures.Where(a => a.Id == id).Include(a => a.Locations).FirstOrDefaultAsync();
            return adventure;
        }

        public async Task<bool> DeleteAdventure(int id)
        {
            var adventure = await context.Adventures.Where(a => a.Id == id).FirstOrDefaultAsync();
            if (adventure == null)
                return false;

            context.Adventures.Remove(adventure);
            return true;
        }
        #endregion

        #region Location Crud
        public async Task<LocationDto> GetLocationById(int id)
        {
            Location location = await context.Locations.Where(l => l.Id == id)
                .Include(n => n.NPCs)
                .Include(c => c.Containers).ThenInclude(i => i.Items).ThenInclude(i => i.Item).ThenInclude(p => p.Photo)
                .Include(i => i.Interactions)
                .Include(t => t.Triggers)
                .FirstOrDefaultAsync();

            if (location == null) return null;

            List<ConnectedLocationDto> connectedLocations = await buildConnectedLocationList(id);
            List<ContainerDto> containers = buildContainerList(location.Containers);

            var locationDto = new LocationDto
            {
                Id = id,
                Name = location.Name,
                Description = location.Description,
                ConnectedLocations = connectedLocations,
                NPCs = location.NPCs,
                Containers = containers,
                Interactions = location.Interactions,
                Triggers = location.Triggers,
                Location = location,
            };

            return locationDto;
        }

        public async Task<LocationSaveDto> GetPlayerLocation(int locationId, int adventureSaveId)
        {
            var adventure = await context.AdventureSaves
                    .Include(a => a.LocationSaves)
                    .FirstOrDefaultAsync(a => a.Id == adventureSaveId);
            if (adventure == null) return null;

            var locationSave = await context.LocationSaves
                .Include(s => s.Location)
                .FirstOrDefaultAsync(l => l.LocationId == locationId && l.AdventureSaveId == adventureSaveId);

            if (locationSave == null)
            {
                var newLocation = await GetLocationById(locationId);
                if (newLocation == null) return null;

                foreach (LocationSave ls in adventure.LocationSaves)
                    ls.IsCurrentLocation = false;

                LocationSave newSave = new LocationSave
                {
                    Location = newLocation.Location,
                    IsCurrentLocation = true
                };
                adventure.LocationSaves.Add(newSave);
                await context.SaveChangesAsync();

                return LocationSaveDto.Convert(newSave, newLocation);

            }
            foreach (LocationSave ls in adventure.LocationSaves)
                ls.IsCurrentLocation = false;
            
            locationSave.IsCurrentLocation = true;
            await context.SaveChangesAsync();

            //If its not null, return it to the user
            return LocationSaveDto.Convert(locationSave, await GetLocationById(locationSave.LocationId));
        }



        public async Task<ContainerDto> GetContainer(int id)
        {
            var container = await context.ContainerCollection
                .Include(c => c.Items).ThenInclude(i => i.Item).ThenInclude(i => i.Photo)
                .FirstOrDefaultAsync(x => x.Id == id);
            return ContainerDto.Convert(container);
        }

        public async Task<Location> CreateLocation(NewLocationDto newLocation, int adventureId)
        {
            Location l = new Location { Name = newLocation.Name, Description = newLocation.Description };
            var adventure = await context.Adventures.Where(a => a.Id == adventureId).FirstOrDefaultAsync();

            if (adventure != null)
            {
                if (adventure.Locations == null)
                    adventure.Locations = new List<Location>();
                adventure.Locations.Add(l);
                return l;
            }
            else return null;

        }

        public async Task<Location> LinkLocation(int fromLocation, int toLocation, string mode = "one-way")
        {
            Location from = await context.Locations.Where(l => l.Id == fromLocation).Include(c => c.ConnectedToLocations).FirstOrDefaultAsync();
            Location to = await context.Locations.Where(l => l.Id == toLocation).FirstOrDefaultAsync();
            if (from != null && to != null)
            {
                from.ConnectedToLocations.Add(new LocationLink
                {
                    FromId = from.Id,
                    ToId = to.Id,
                });
                await context.SaveChangesAsync();

                if (mode.ToLower() == "two-way")
                {

                    await LinkLocation(toLocation, fromLocation);
                }

                return from;
            }
            return null;
        }

        public async Task<bool> DeleteLocation(int locationId, int adventureId)
        {

            var adventure = await context.Adventures.Include(a => a.Locations).Where(a => a.Id == adventureId).FirstOrDefaultAsync();
            if (adventure == null)
                return false;

            var location = adventure.Locations.Where(l => l.Id == locationId).FirstOrDefault();
            if (location == null)
                return false;

            adventure.Locations.Remove(location);
            context.Locations.Remove(location);

            return true;
        }

        private async Task<List<ConnectedLocationDto>> buildConnectedLocationList(int locationId)
        {
            List<LocationLink> links = await context.LocationLink.Where(l => l.FromId == locationId).ToListAsync();
            List<ConnectedLocationDto> connectedLocations = new List<ConnectedLocationDto>();
            foreach (LocationLink link in links)
            {
                Location to = await context.Locations.Where(l => l.Id == link.ToId).FirstOrDefaultAsync();
                if (to == null) return null;

                connectedLocations.Add(new ConnectedLocationDto
                {
                    Id = to.Id,
                    Name = to.Name,
                    Description = to.Description
                });
            }

            return connectedLocations;
        }

        private List<ContainerDto> buildContainerList(List<Container> containers)
        {
            List<ContainerDto> containerList = new List<ContainerDto>();
            foreach (Container c in containers)
            {
                List<ItemDto> items = new List<ItemDto>();
                foreach (ContainerItem i in c.Items)
                    items.Add(ItemDto.Convert(i.Item));

                ContainerDto containerDto = new ContainerDto
                {
                    Id = c.Id,
                    Name = c.Name,
                    Description = c.Description,
                    Items = items
                };

                containerList.Add(containerDto);
            }
            return containerList;
        }

        #endregion

        #region Interaction CRUD
        public async Task<Interaction> CreateInteraction(NewInteractionDto newInteractionDto)
        {
            Location location = await context.Locations.Include(l => l.Interactions).FirstOrDefaultAsync(l => l.Id == newInteractionDto.LocationId);
            if (location == null) return null;

            Interaction interaction = new Interaction
            {
                Name = newInteractionDto.Name,
                Information = newInteractionDto.Information,
            };

            location.Interactions.Add(interaction);
            return interaction;
        }

        public async Task<Interaction> GetInteraction(int id)
        {
            Interaction interaction = await context.Interactions.FirstOrDefaultAsync(i => i.Id == id);
            if (interaction == null) return null;

            return interaction;
        }

        public async Task<bool> DeleteInteraction(int id)
        {
            var interaction = await context.Interactions.FirstOrDefaultAsync(i => i.Id == id);
            if (interaction == null) return false;

            var location = await context.Locations.Include(l => l.Interactions).FirstOrDefaultAsync(l => l.Interactions.Contains(interaction));
            if (location != null)
                location.Interactions.Remove(interaction);

            context.Interactions.Remove(interaction);
            return true;

        }
        #endregion

        #region Adventure Saves

        public async Task<AdventureSaveDto> CreateAdventureSave(NewAdventureSave newSave, int userId)
        {
            var adventure = await context.Adventures
                .Include(a => a.StartingLocation)
                .FirstOrDefaultAsync(a => a.Id == newSave.AdventureId);
            if (adventure == null) { return null; }

            //Get starting location, and add to list of location saves
            var location = await context.Locations
                .FirstOrDefaultAsync(l => l.Id == adventure.StartingLocation.Id);
            if (location == null) return null;

            LocationSave locationSave = new LocationSave
            {
                Location = location,
                IsCurrentLocation = true,
            };

            AdventureSave save = new AdventureSave
            {
                SaveDescription = newSave.SaveDescription,
                Adventure = adventure,
            };

            save.LocationSaves = new List<LocationSave> { locationSave };

            var user = await context.Users
                .Include(a => a.AdventureSaves)
                .FirstOrDefaultAsync(u => u.Id == userId);
            if (user == null) return null;

            user.AdventureSaves.Add(save);

            AdventureSaveDto dto = AdventureSaveDto.Convert(save);
            dto.CurrentLocation = LocationSaveDto.Convert(locationSave);

            return dto;
        }

        public async Task<bool> DeleteAdventureSave(int id)
        {
            var adventureSave = await context.AdventureSaves
                .Include(l => l.LocationSaves)
                .FirstOrDefaultAsync(a => a.Id == id);
            if (adventureSave == null) return false;

            context.AdventureSaves.Remove(adventureSave);
            return true;
        }

        public async Task<AdventureSaveDto> GetAdventureSave(int id)
        {
            var save = await context.AdventureSaves
                .Include(a => a.PlayerCharacters)
                .Include(a => a.Adventure)
                .Include(a => a.LocationSaves).ThenInclude(l => l.Location)
                .FirstOrDefaultAsync(a => a.Id == id);

            if (save == null) return null;
            AdventureSaveDto dto = AdventureSaveDto.Convert(save);
            return dto;
        }

        public async Task<bool> AddPlayerCharacterToAdventure(int playerCharacterId, int adventureSaveId)
        {
            var pc = await context.PlayerCharacters.FirstOrDefaultAsync(p => p.Id == playerCharacterId);
            if (pc == null) return false;

            var save = await context.AdventureSaves
                .Include(a => a.PlayerCharacters)
                .FirstOrDefaultAsync(a => a.Id == adventureSaveId);
            if (save == null) return false;

            save.PlayerCharacters.Add(pc);
            return true;
        }

        public async Task<AdventureSaveDto> RemovePlayerCharacterFromAdventure(int playerCharacterId, int adventureSaveId)
        {
            var pc = await context.PlayerCharacters.FirstOrDefaultAsync(p => p.Id == playerCharacterId);
            if (pc == null) return null;

            var save = await context.AdventureSaves
                .Include(a => a.PlayerCharacters)
                .FirstOrDefaultAsync(a => a.Id == adventureSaveId);
            if (save == null) return null;

            save.PlayerCharacters.Remove(pc);
            await context.SaveChangesAsync();
            return await GetAdventureSave(adventureSaveId);
        }

        public async Task<LocationSave> SavePlayerLocation(int id)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Location Saves

        #endregion
    }
}
