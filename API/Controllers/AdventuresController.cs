﻿using API.DTOs.Adventure;
using API.Entities.Adventure;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class AdventuresController : BaseApiController
    {
        private readonly UnitOfWork uow;

        public AdventuresController(UnitOfWork uow)
        {
            this.uow = uow;
        }

        #region Adventure Crud
        [HttpPost("create-adventure")]
        public async Task<ActionResult<Adventure>> CreateAdventure(Adventure newAdventure)
        {
            Adventure adventure = await uow.AdventureRepository.CreateAdventure(newAdventure);

            if (await uow.Complete())
                return Ok(adventure);

            return BadRequest("Failed to create adventure");
        }

        [HttpGet("get-available")]
        public async Task<ActionResult<List<Adventure>>> GetAdventures()
        {
            return await uow.AdventureRepository.GetAvailableAdventures();
        }


        [HttpDelete("delete")]
        public async Task<ActionResult> Delete([FromQuery] int Id)
        {
            if (await uow.AdventureRepository.DeleteAdventure(Id))
            {
                if (await uow.Complete())
                    return Ok();
            }
            return BadRequest("Failed to delete adventure");
        }
        #endregion

        #region Location Crud
        [HttpPost("create-location")]
        public async Task<ActionResult<LocationDto>> CreateLocation(NewLocationDto newLocation, [FromQuery] int adventureId)
        {
            Location location = await uow.AdventureRepository.CreateLocation(newLocation, adventureId);

            if (location == null) return BadRequest("Failed to find adventure to bind location to. Verify ID is correct");
            if (await uow.Complete())
                return Ok(location);

            return BadRequest("Failed to create location");
        }

        [HttpPost("link-location")]
        public async Task<ActionResult<string>> LinkLocation([FromQuery] int fromLocation, [FromQuery] int toLocation, [FromQuery] string mode)
        {
            Location l = await uow.AdventureRepository.LinkLocation(fromLocation, toLocation, mode);

            if (l != null)
                return Ok();

            return BadRequest("Failed to link location");
        }

        [HttpGet("get-location")]
        public async Task<ActionResult<LocationDto>> GetLocation(int id)
        {
            LocationDto l = await uow.AdventureRepository.GetLocationById(id);
            if (l == null) return BadRequest("That location does not exist");
            return Ok(l);
        }

        [HttpGet("get-player-location")]
        public async Task<ActionResult<LocationDto>> GetPlayerLocation(int id)
        {
            LocationDto l = await uow.AdventureRepository.GetLocationById(id);
            if (l == null) return BadRequest("That location does not exist");
            return Ok(l);
        }



        [HttpDelete("delete-location")]
        public async Task<ActionResult> DeleteLocation([FromQuery] int locationId, [FromQuery] int adventureId)
        {
            if (await uow.AdventureRepository.DeleteLocation(locationId, adventureId))
            {
                if (await uow.Complete())
                    return Ok();
            }
            return BadRequest("Failed to delete location");
        }
        #endregion

        #region Container CRUD
        [HttpPost("create-container")]
        public async Task<ActionResult<Container>> CreateContainer(NewContainerDto newContainer)
        {
            var container = await uow.ContainerRepository.CreateContainer(newContainer);
            if (container == null) return BadRequest("Failed to create container");
            if (await uow.Complete())
                return Ok(container);

            return BadRequest("Something went wrong");
        }

        [HttpGet("get-containers")]
        public async Task<ActionResult<List<ContainerDto>>> GetContainers([FromQuery] int locationId)
        {
            var containers = await uow.ContainerRepository.GetContainers(locationId);
            if (containers == null) return NoContent();

            return containers;
        }

        [HttpGet("get-container")]
        public async Task<ActionResult<ContainerDto>> GetContainer([FromQuery] int id)
        {
            ContainerDto dto = await uow.AdventureRepository.GetContainer(id);
            if (dto == null) return BadRequest("Could not find container");
            return Ok(dto);
        }

        [HttpPost("add-item-to-container")]
        public async Task<ActionResult<Container>> AddItemToContainer(int containerId, int itemId)
        {
            var container = await uow.ContainerRepository.AddItemToContainer(containerId, itemId);

            if (container == null) return BadRequest("Container or item do not exist");

            if (await uow.Complete())
                return Ok(container);

            return BadRequest("Could not add item to container");
        }

        [HttpDelete("delete-item-from-container")]
        public async Task<ActionResult<ContainerDto>> DeleteItemFromContainer(int containerId, int itemId)
        {
            var container = await uow.ContainerRepository.DeleteItemFromContainer(containerId, itemId);

            if (container == null) return BadRequest("Container or item do not exist");

            if (await uow.Complete())
                return Ok(container);

            return BadRequest("Could not delete item from container");
        }

        [HttpDelete("delete-container")]
        public async Task<ActionResult> DeleteContainer([FromQuery] int containerId)
        {
            if (!await uow.ContainerRepository.DeleteContainer(containerId)) return BadRequest("Could not find container");

            if (await uow.Complete())
                return Ok();

            return BadRequest("Failed to delete container");
        }
        #endregion

        #region Interaction CRUD
        [HttpPost("create-interaction")]
        public async Task<ActionResult<Interaction>> CreateInteraction(NewInteractionDto newInteraction)
        {
            var interaction = await uow.AdventureRepository.CreateInteraction(newInteraction);
            if (interaction == null) return BadRequest("Could not find location");

            if (await uow.Complete())
                return Ok(interaction);

            return BadRequest("Failed to create interaction");
        }

        [HttpGet("get-interaction")]
        public async Task<ActionResult<Interaction>> GetInteraction([FromQuery] int id)
        {
            var interaction = await uow.AdventureRepository.GetInteraction(id);
            if (interaction == null) return NoContent();

            return Ok(interaction);
        }

        [HttpDelete("delete-interaction")]
        public async Task<ActionResult> DeleteInteraction(int id)
        {
            if (!await uow.AdventureRepository.DeleteInteraction(id)) return BadRequest("Could not delete interaction. It may not exist");
            if (await uow.Complete())
                return Ok();

            return BadRequest("Failed to delete interaction");
        }
        #endregion
    }
}