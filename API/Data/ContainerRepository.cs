using API.Entities;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class ContainerRepository
    {
        private readonly DataContext context;
        private readonly IMapper mapper;

        public ContainerRepository(DataContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<Container> CreateContainer(NewContainerDto newContainer)
        {
            Location l = await context.Locations.Where(l => l.Id == newContainer.LocationId).Include(l => l.Containers).FirstOrDefaultAsync();
            if (l == null) return null;

            Container c = new Container
            {
                Name = newContainer.Name,
                Description = newContainer.Description,
            };

            l.Containers.Add(c);
            return c;
        }

        public async Task<List<ContainerDto>> GetContainers(int locationId)
        {
            var location = await context.Locations.Where(l => l.Id == locationId)
                .Include(l => l.Containers)
                .ThenInclude(container => container.Items).ThenInclude(i => i.Item)
                .ThenInclude(item => item.Photo)
                .FirstOrDefaultAsync();

            List<ContainerDto> containers = new List<ContainerDto>();

            foreach (Container container in location.Containers)
            {
                List<ItemDto> items = new List<ItemDto>();
                foreach (ItemContainerLink i in container.Items)
                    items.Add(ItemDto.Convert(i.Item));

                ContainerDto c = new ContainerDto
                {
                    Id = container.Id,
                    Name = container.Name,
                    Description = container.Description,
                    Items = items
                };

                containers.Add(c);
            }

            return containers;
        }

        public async Task<ContainerDto> AddItemToContainer(int containerId, int itemId)
        {
            var itemToAdd = await context.ItemCollection.Where(i => i.Id == itemId).FirstOrDefaultAsync();
            var container = await context.ContainerCollection.Where(c => c.Id == containerId)
                .Include(i => i.Items).ThenInclude(i => i.Item).ThenInclude(i => i.Photo)
                .FirstOrDefaultAsync();

            if (itemToAdd == null || container == null) return null;

            container.Items.Add(new ItemContainerLink { Item = itemToAdd, Container = container });

            List<ItemDto> items = new List<ItemDto>();
            foreach (ItemContainerLink link in container.Items)
            {
                Item item = link.Item;
                items.Add(new ItemDto
                {
                    Id = item.Id,
                    Name = item.Name,
                    RequiredLevel = item.RequiredLevel,
                    PhotoUrl = item.Photo.Url,
                    AttackValue = item.AttackValue,
                    ArmorValue = item.ArmorValue,
                    StatModifiers = item.StatModifiers,
                    DamageModifiers = item.DamageModifiers,
                    ResistanceModifiers = item.ResistanceModifiers,
                    ItemType = item.ItemType,
                    DamageType = item.DamageType,
                });
            }

            ContainerDto c = new ContainerDto
            {
                Id = containerId,
                Name = container.Name,
                Description = container.Description,
                Items = items
            };

            return c;
        }

        public async Task<ContainerDto> DeleteItemFromContainer(int containerId, int itemId)
        {
            var container = await context.ContainerCollection.Where(c => c.Id == containerId)
                .Include(i => i.Items).ThenInclude(i => i.Item).ThenInclude(i => i.Photo)
                .FirstOrDefaultAsync();

            var itemLink = container.Items.Where(i => i.Id == itemId).FirstOrDefault();

            if (container == null || itemLink == null) return null;

            container.Items.Remove(itemLink);

            List<ItemDto> items = new List<ItemDto>();
            foreach (ItemContainerLink i in container.Items)
                items.Add(ItemDto.Convert(i.Item));

            ContainerDto c = new ContainerDto
            {
                Id = containerId,
                Name = container.Name,
                Description = container.Description,
                Items = items
            };

            return c;
        }

        public async Task<bool> DeleteContainer(int containerId)
        {
            var container = await context.ContainerCollection.Where(c => c.Id == containerId).FirstOrDefaultAsync();
            if (container == null) return false;

            context.ContainerCollection.Remove(container);

            return true;

        }


    }
}
