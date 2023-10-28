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

        public async Task<List<Adventure>> GetAvailableAdventures()
        {
            return await context.Adventures.Include(l => l.Locations).Include(a => a.StartingLocation).ToListAsync();
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
            var links = await context.LocationLink.Where(l => l.FromId == id).ToListAsync();

            Location location = await context.Locations.Where(l => l.Id == id)
                .Include(n => n.NPCs)
                .Include(c => c.Containers).ThenInclude(i => i.Items).ThenInclude(i => i.Item).ThenInclude(p=> p.Photo)
                .Include(i => i.Interactions)
                .FirstOrDefaultAsync();

            if (location == null) return null;

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

            List<ContainerDto> containers = new List<ContainerDto>();
            foreach (Container c in location.Containers)
            {
                List<ItemDto> items = new List<ItemDto>();
                foreach(ContainerItem i in c.Items)
                    items.Add(ItemDto.Convert(i.Item));

                ContainerDto containerDto = new ContainerDto
                {
                    Id = c.Id,
                    Name = c.Name,
                    Description = c.Description,
                    Items = items
                };


                containers.Add(containerDto);
            }

            var locationDto = new LocationDto
            {
                Id = id,
                Name = location.Name,
                Description = location.Description,
                ConnectedLocations = connectedLocations,
                NPCs = location.NPCs,
                Containers = containers,
                Interactions = location.Interactions,
            };

            return locationDto;
        }

        public async Task<LocationDto> GetPlayerLocation(int id)
        {
            return await GetLocationById(id);
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
                    ToId= to.Id,
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
            if(interaction == null) return false;

            var location = await context.Locations.Include(l => l.Interactions).FirstOrDefaultAsync(l => l.Interactions.Contains(interaction));
            if (location != null)
                location.Interactions.Remove(interaction);

            context.Interactions.Remove(interaction);
            return true;
            
        }
        #endregion
    }
}
