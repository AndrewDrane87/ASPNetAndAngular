using API.DTOs.Admin;
using API.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.ObjectPool;

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

        #region Admin
        public async Task<List<AdminAdventureDto>> AdminGetAdventures()
        {
            var adventures = await context.Adventures
                .Include(a => a.Locations)
                .Include(a => a.StartingLocation)
                .Include(a => a.Variables)
                .ToListAsync();
            return AdminAdventureDto.ConvertList(adventures);
        }

        #endregion

        #region Adventure Crud
        public async Task<Adventure> CreateAdventure(Adventure adventure)
        {
            await context.Adventures.AddAsync(adventure);
            return adventure;
        }



        public async Task<List<AdventureSaveDto>> GetAvailableAdventures(int userId)
        {
            var user = await context.Users
                .Include(u => u.AdventureSaves).ThenInclude(a => a.Adventure).ThenInclude(a => a.StartingLocation)
                .Include(u => u.AdventureSaves).ThenInclude(a => a.PlayerCharacters)
                .Include(u => u.AdventureSaves).ThenInclude(a => a.LocationSaves).ThenInclude(ls => ls.Location)
                .FirstOrDefaultAsync(u => u.Id == userId);

            foreach (AdventureSave save in user.AdventureSaves)
            {
                if (save.LocationSaves.Count == 0)
                    save.LocationSaves.Add(await CreateNewLocationSave(save.Adventure.StartingLocation.Id));
            }

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

            return LocationDto.Convert(location, connectedLocations, containers);
        }

        public async Task<LocationSaveDto> GetPlayerLocation(int locationId, int adventureSaveId)
        {
            var adventure = await context.AdventureSaves
                    .Include(a => a.LocationSaves).ThenInclude(l => l.Enemies)
                    .Include(a => a.PlayerCharacters)
                    .Include(a => a.LocationSaves).ThenInclude(l => l.Containers)
                    .FirstOrDefaultAsync(a => a.Id == adventureSaveId);
            if (adventure == null) return null;

            var locationSave = await checkForRequiredLocationSaves(locationId, adventure);


            //Set all location saves is current location to false, since we are no longer there 
            //And need to set the new location as current
            foreach (LocationSave ls in adventure.LocationSaves)
                ls.IsCurrentLocation = false;

            locationSave.IsCurrentLocation = true;
            adventure.CurrentLocation = locationSave;

            locationSave = await CheckForEnemySaves(locationSave, adventure);
            locationSave = await CheckForContainerSaves(locationSave, adventure);

            LocationSaveDto dto = LocationSaveDto.Convert(locationSave, await GetLocationById(locationSave.LocationId));

            List<ConnectedLocationDto> filteredConnectedLocations = new List<ConnectedLocationDto>();
            foreach (var connectedLocationDto in dto.ConnectedLocations)
                if (await CheckVisibility(connectedLocationDto.VisibilityRequirements, adventure.Id))
                    filteredConnectedLocations.Add(connectedLocationDto);
            dto.ConnectedLocations = filteredConnectedLocations;

            return dto;
        }

        public async Task<LocationSave> checkForRequiredLocationSaves(int locationId, AdventureSave adventure)
        {
            //Check to see if a save exists for this location.
            var locationSave = await context.LocationSaves
                .Include(s => s.Location)
                .Include(l => l.Triggers)
                .FirstOrDefaultAsync(l => l.LocationId == locationId && l.AdventureSaveId == adventure.Id);

            //If we didnt find the location save it means we have to make one.
            if (locationSave == null)
            {
                locationSave = await CreateNewLocationSave(locationId);
                adventure.LocationSaves.Add(locationSave);
            }

            foreach (var connectedLocation in await buildConnectedLocationList(locationId))
            {
                //Check to see if a save exists for this location.
                var connectedLocationSave = await context.LocationSaves
                    .Include(s => s.Location)
                    .FirstOrDefaultAsync(l => l.LocationId == connectedLocation.Id && l.AdventureSaveId == adventure.Id);

                //If we didnt find the location save it means we have to make one.
                if (connectedLocationSave == null)
                {
                    connectedLocationSave = await CreateNewLocationSave(connectedLocation.Id);
                    adventure.LocationSaves.Add(connectedLocationSave);
                }
            }

            return locationSave;
        }

        public async Task<LocationSave> CreateNewLocationSave(int locationId)
        {
            var newLocation = await GetLocationById(locationId);
            if (newLocation == null) return null;

            List<ActionTriggerSave> triggerSaves = CreateActionTriggerSavesForLocation(newLocation);

            LocationSave newSave = new LocationSave
            {
                Location = newLocation.Location,
                IsCurrentLocation = true,
                Triggers = triggerSaves,
                VisibilityRequirement = newLocation.VisibilityRequirements
            };

            return newSave;
        }

        public List<ActionTriggerSave> CreateActionTriggerSavesForLocation(LocationDto location)
        {
            List<ActionTriggerSave> list = new List<ActionTriggerSave>();
            foreach (ActionTrigger trigger in location.Triggers)
            {
                ActionTriggerSave save = new ActionTriggerSave()
                {
                    ActionTrigger = trigger,
                    Complete = false,
                };
                list.Add(save);
            }

            return list;
        }


        public async Task<ContainerDto> GetContainer(int id)
        {
            var container = await context.Containers
                .Include(c => c.Items).ThenInclude(i => i.Item).ThenInclude(i => i.Photo)
                .FirstOrDefaultAsync(x => x.Id == id);

            return ContainerDto.Convert(container);
        }

        public async Task<ContainerSaveDto> GetPlayerContainer(int containerSaveId)
        {
            var save = await context.ContainerSaves
                .Include(c => c.Container)
                .Include(c => c.Items).ThenInclude(items => items.Item).ThenInclude(i => i.Photo)
                .FirstOrDefaultAsync(c => c.Id == containerSaveId);

            if (save.Complete == false)
            {
                save.Items = await CheckForItemSaves(save);
                save.Complete = true;
                await context.SaveChangesAsync();
            }

            return ContainerSaveDto.Convert(save);
        }

        public async Task<List<ItemSave>> CheckForItemSaves(ContainerSave containerSave)
        {
            var baseContainer = await context.Containers
                .Include(c => c.Items).ThenInclude(i => i.Item).ThenInclude(i => i.Photo)
                .FirstOrDefaultAsync(c => c.Id == containerSave.ContainerId);

            List<ItemSave> saves = new List<ItemSave>();
            foreach (ItemContainerLink i in baseContainer.Items)
            {
                var itemSave = containerSave.Items.FirstOrDefault(save => save.ItemId == i.ItemId);
                if (itemSave == null)
                {
                    itemSave = new ItemSave
                    {
                        Item = i.Item,
                        CurrentStackSize = i.Item.StackSize,
                        ContainerSave = containerSave,
                    };
                }
                saves.Add(itemSave);
            }

            return saves;
        }



        public async Task<Location> CreateLocation(NewLocationDto newLocation, int adventureId)
        {
            Location l = new Location { Name = newLocation.Name, ShortDescription = newLocation.ShortDescription, Description = newLocation.Description, RoomNumber = newLocation.RoomNumber };
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
                connectedLocations.Add(ConnectedLocationDto.Convert(to));
            }

            return connectedLocations;
        }

        private List<ContainerDto> buildContainerList(List<Container> containers)
        {
            List<ContainerDto> containerList = new List<ContainerDto>();
            foreach (Container c in containers)
            {
                List<ItemDto> items = new List<ItemDto>();
                foreach (ItemContainerLink i in c.Items)
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
                .Include(a => a.Adventure).ThenInclude(a => a.StartingLocation)
                .Include(a => a.LocationSaves).ThenInclude(l => l.Location)
                .Include(a => a.CurrentLocation).ThenInclude(l => l.Location)
                .FirstOrDefaultAsync(a => a.Id == id);

            if (save.CurrentLocation == null)
                await checkForRequiredLocationSaves(save.Adventure.StartingLocation.Id, save);

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

        public async Task<bool> UpdateTriggerSave(int triggerSaveId, bool isComplete, string result)
        {
            var save = await context.ActionTriggerSaves
                .Include(t => t.ActionTrigger)
                .Include(l => l.LocationSave)
                .FirstOrDefaultAsync(t => t.Id == triggerSaveId);
            if (save == null) return false;

            //These are additional actions that can be built into the trigger.
            if (save.ActionTrigger.ResultData != null)
            {
                foreach (string s in save.ActionTrigger.ResultData.Split('|'))
                {
                    switch (s.Split(':')[0].ToLower())
                    {
                        case "setvariable": await SetVariableValue(s, save.LocationSave.AdventureSaveId, result); break;
                    }
                }
            }

            save.Complete = isComplete;
            save.Result = result;

            return true;
        }


        public async Task<bool> CheckVisibility(string visibilityRequirement, int adventureSaveId)
        {
            if (visibilityRequirement == null) return true;
            if (visibilityRequirement.Length == 0) return true;
            /*
             [variableName]:[value]
             */

            string[] values = visibilityRequirement.Split(':');
            string variableName = values[0];
            string variableValue = values[1];

            var adventure = await context.AdventureSaves
                .Include(a => a.Variables).ThenInclude(v => v.AdventureVariable)
                .FirstOrDefaultAsync(a => a.Id == adventureSaveId);

            var variable = adventure.Variables.FirstOrDefault(v => v.AdventureVariable.Name == variableName);
            //If we dont find the variable in the save collection we need to create it first
            if (variable == null)
                variable = await CreateVariableSave(adventure, variableName);

            return variable.Value == variableValue;
        }

        public async Task<AdventureVariableSave> SetVariableValue(string variableData, int adventureSaveId, string result)
        {
            /* Example Result data formats
             * Assume its a boolean and we are setting it to true. Length 2
             * setVariable:VariableName
             * new value included in the data. Length 3
             * setVariable:VariableName:variableValue
             * Coming from a success fail check. Length 4
             * setVariable:VariableName:VariableValueOnSuccess:VariableValueOnFail
             */

            string[] strings = variableData.Split(":");
            var adventureSave = await context.AdventureSaves
                .Include(v => v.Variables).ThenInclude(v => v.AdventureVariable)
                .FirstOrDefaultAsync(a => a.Id == adventureSaveId);

            var variable = adventureSave.Variables.FirstOrDefault(v => v.AdventureVariable.Name == strings[1]);

            if (variable == null) variable = await CreateVariableSave(adventureSave, strings[1]);

            switch (strings.Length)
            {
                case 2: break;
                case 3: break;
                case 4: variable.Value = result == "Success" ? strings[2] : strings[3]; break;
            }
            return variable;
        }

        public async Task<AdventureVariableSave> CreateVariableSave(AdventureSave adventure, string variableName)
        {
            //Grab the base adventure object so we can get its variable list.
            var baseAdventure = await context.Adventures
                .Include(a => a.Variables)
                .FirstOrDefaultAsync(a => a.Id == adventure.AdventureId);

            //Get the variable from the base object
            var baseVariable = baseAdventure.Variables.FirstOrDefault(v => v.Name == variableName);

            //Create a new save and add it to our save collection
            var variable = new AdventureVariableSave { Value = baseVariable.InitialValue, AdventureVariable = baseVariable };
            adventure.Variables.Add(variable);
            await context.SaveChangesAsync();
            return variable;
        }

        public async Task<LocationSave> CheckForEnemySaves(LocationSave locationSave, AdventureSave adventureSave)
        {


            var baseLocation = await context.Locations
                .Include(l => l.EnemyLocationLinks).ThenInclude(link => link.Enemy).ThenInclude(e => e.Photo)
                .FirstOrDefaultAsync(l => l.Id == locationSave.LocationId);

            List<EnemySave> enemySaves = new List<EnemySave>();
            foreach (EnemyLocationLink link in baseLocation.EnemyLocationLinks)
            {
                var enemySave = locationSave.Enemies.FirstOrDefault(save => save.EnemyLocationLinkId == link.Id);
                if (enemySave == null)
                {
                    enemySave = new EnemySave
                    {
                        EnemyId = link.Enemy.Id,
                        Enemy = link.Enemy,
                        CurrentHp = adventureSave.PlayerCharacters.Count > link.RequiredPlayerCount ? link.Enemy.MaxHp : -1,
                        EnemyLocationLink = link,
                        
                    };
                }
                enemySaves.Add(enemySave);
            }
            locationSave.Enemies = enemySaves;
            await context.SaveChangesAsync();

            return locationSave;
        }

        public async Task<bool> DealDamage(int damageAmount, int enemyId)
        {
            if (damageAmount < 0)
                damageAmount = 0;

            var enemySave = await context.EnemySaves.FirstOrDefaultAsync(e => e.Id == enemyId);
            if (enemySave == null) return false;

            enemySave.CurrentHp -= damageAmount;
            return true;
        }

        public async Task<LocationSave> CheckForContainerSaves(LocationSave locationSave, AdventureSave adventureSave)
        {
            var baseLocation = await context.Locations
                .Include(l => l.Containers).ThenInclude(c => c.Items)
                .Include(l => l.Containers).ThenInclude(c => c.Triggers)
                .FirstOrDefaultAsync(l => l.Id == locationSave.LocationId);

            List<ContainerSave> saves = new List<ContainerSave>();
            foreach (Container c in baseLocation.Containers)
            {
                var containerSave = locationSave.Containers.FirstOrDefault(save => save.ContainerId == c.Id);
                if (containerSave == null)
                {
                    containerSave = new ContainerSave
                    {
                        ContainerId = c.Id,
                        Container = c,
                        Complete = false
                    };
                }
                saves.Add(containerSave);
            }
            locationSave.Containers = saves;
            await context.SaveChangesAsync();
            return locationSave;
        }

        public async Task<List<ItemSaveDto>> GetAvailableItems(int LocationSaveId)
        {
            var locationSave = await context.LocationSaves
                .Include(l => l.Containers).ThenInclude(c => c.Items).ThenInclude(items => items.Item).ThenInclude(i => i.Photo)
                .Include(l => l.Items).ThenInclude(i => i.Item.Photo)
                .FirstOrDefaultAsync(l => l.Id == LocationSaveId);

            List<ItemSave> items = new List<ItemSave>();
            foreach (ContainerSave container in locationSave.Containers.Where(c => c.Complete))
                foreach (ItemSave item in container.Items)
                    items.Add(item);

            foreach (ItemSave i in locationSave.Items)
                items.Add(i);

            return ItemSaveDto.ConvertList(items);
        }

        public async Task<List<ItemSaveDto>> GetItems(int characterId, string itemType, int currentItemId = -1)
        {
            var pc = await context.PlayerCharacters
                .Include(p => p.BackPack).ThenInclude(b => b.Item).ThenInclude(i => i.Photo)
                .FirstOrDefaultAsync(p => p.Id == characterId);
            var adventureSave = await context.AdventureSaves.FirstOrDefaultAsync(a => a.Id == pc.AdventureSaveId);
            var locationSave = await context.LocationSaves.FirstOrDefaultAsync(l => l.AdventureSaveId == adventureSave.Id && l.IsCurrentLocation);

            if (pc == null || adventureSave == null || locationSave == null)
                return null;

            List<ItemSaveDto> items = await GetAvailableItems(locationSave.Id);

            //foreach (ItemSave backPackItem in pc.BackPack.Where(i => i.Id != currentItemId))
            //  if (backPackItem != null)
            //    items.Add(ItemSaveDto.Convert(backPackItem));

            if (itemType == "any")
                return items.OrderBy(i => i.ItemType).ToList();

            if (itemType == "hand")
                return items.Where(i => i.ItemType == "sword" || i.ItemType == "shield").OrderBy(i => i.ItemType).ToList();


            return items.Where(i => i.ItemType == itemType).OrderBy(i => i.ItemType).ToList();

        }

        public async Task ResetSaves()
        {
            //Reset trigger saves
            var triggerSaves = await context.ActionTriggerSaves.ToListAsync();
            foreach (ActionTriggerSave save in triggerSaves)
                save.Complete = false;

            //Reset Variables
            var adventure = await context.AdventureSaves
                .Include(a => a.Variables)
                .Include(a => a.LocationSaves).ThenInclude(l => l.Containers)
                .FirstOrDefaultAsync(a => a.Id == 1);

            adventure.Variables.Clear();

            //Reset the current location
            var baseAdventure = await context.Adventures
                .Include(a => a.StartingLocation)
                .FirstOrDefaultAsync(a => a.Id == adventure.AdventureId);
            foreach (var location in adventure.LocationSaves)
            {
                location.IsCurrentLocation = false;
                location.Containers.Clear();
            }

            var startingLocationId = baseAdventure.StartingLocation.Id;
            adventure.LocationSaves.FirstOrDefault(l => l.LocationId == startingLocationId).IsCurrentLocation = true;

            await context.SaveChangesAsync();
        }
    }
}
