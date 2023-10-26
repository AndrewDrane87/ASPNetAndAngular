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
            return await context.Adventures.Include(l => l.Locations).ToListAsync();
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
                {
                    Item item = i.Item;
                    items.Add(new ItemDto
                    {
                        Id = item.Id,
                        Name = item.Name,
                        RequiredLevel = item.RequiredLevel,
                        PhotoUrl = item.Photo.Url,
                        AttackValue = item.AttackValue,
                        ArmorValue = item.ArmorValue,
                        Modifiers = item.Modifiers,
                        ItemType = item.ItemType,
                        DamageType = item.DamageType,
                    });
                }

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
                Containers = containers
            };

            return locationDto;
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
    }
}
