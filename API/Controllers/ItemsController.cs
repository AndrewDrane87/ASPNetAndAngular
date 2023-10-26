using API.Controllers;
using API.DTOs.Items;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.IIS.Core;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Runtime.CompilerServices;

namespace API;

public class ItemsController : BaseApiController
{
    private readonly UnitOfWork uow;
    private readonly IPhotoService photoService;

    public ItemsController(UnitOfWork uow, IPhotoService photoService)
    {
        this.uow = uow;
        this.photoService = photoService;
    }

    [HttpPost("create")]
    public async Task<ActionResult<Item>> Create(NewItemDto item)
    {
        Item i = await uow.ItemRepository.CreateItem(item);
        if (i == null) return BadRequest("failed to create item");
        if (await uow.Complete())
        {
            return Ok(i);
        }

        return BadRequest("Something went wrong on the server.");
    }

    [HttpGet("get")]
    public async Task<ActionResult<List<ItemDto>>> GetItems([FromQuery] string itemType)
    {
        var items = await uow.ItemRepository.GetItems(itemType == null ? "any" : itemType);
        if (items != null)
            return items;

        return NoContent();
    }

    [HttpDelete("delete")]
    public async Task<ActionResult> DeleteItem([FromQuery] int itemId)
    {
        if (await uow.ItemRepository.DeleteItem(itemId) && await uow.Complete())
            return Ok();

        return BadRequest("Failed to delete item");

    }




    #region Photos

    [HttpPost("add-item-photo")]
    public async Task<ActionResult<ItemPhoto>> AddHandItemPhoto(IFormFile file, [FromQuery] string itemType)
    {
        var result = await photoService.AddItemPhotoAsync(file);
        if (result.Error != null) return BadRequest(result);

        var photo = new ItemPhoto
        {
            Url = result.SecureUrl.AbsoluteUri,
            ItemType = itemType,
            PublicId = result.PublicId
        };

        uow.context.ItemPhotoCollection.Add(photo);
        if (await uow.Complete())
        {
            //Incorrect response for a rest api
            //return mapper.Map<PhotoDto>(photo);
            return CreatedAtAction(nameof(AddHandItemPhoto), photo);
        }

        return BadRequest("Error adding photo");
    }

    [HttpGet("get-item-photos")]
    public async Task<ActionResult<List<ItemPhoto>>> GetHandItemPhotos([FromQuery] string itemType)
    {
        List<ItemPhoto> itemPhotos;
        if (itemType == null)
            itemPhotos = await uow.context.ItemPhotoCollection.ToListAsync();
        else
            itemPhotos = uow.context.ItemPhotoCollection.Where(i => i.ItemType == itemType).ToList();

        if (itemPhotos == null || itemPhotos.Count == 0)
            return NoContent();

        return Ok(itemPhotos);
    }

    [HttpDelete("delete-item-photo/{id}")]
    public async Task<ActionResult> DeleteItemPhoto(int id)
    {
        var photo = await uow.context.ItemPhotoCollection.Where(p => p.Id == id).FirstOrDefaultAsync();
        if (photo == null) return NotFound("Could not find that item in the photo context");

        if (photo.PublicId != null)
        {
            var result = await photoService.DeletePhotoAsync(photo.PublicId);
            if (result.Error != null) return BadRequest(result.Error.Message);
        }

        uow.context.ItemPhotoCollection.Remove(photo);
        if (await uow.Complete())
            return NoContent();

        return BadRequest("failed to remove image");
    }
    #endregion











}
