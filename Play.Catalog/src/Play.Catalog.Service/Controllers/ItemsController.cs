using Microsoft.AspNetCore.Mvc;
using Play.Catalog.Service.Entities;
using Play.Catalog.Service.Repositories;
using ZstdSharp.Unsafe;

namespace Play.Catalog.Service.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ItemsController : ControllerBase
{
    private readonly ItemRepository ItemRepository = new();

    ////mock
    //private static readonly List<ItemDto> Items = new()
    //{
    //    new ItemDto(Guid.NewGuid(), "Test", "description", 5, DateTimeOffset.UtcNow),
    //    new ItemDto(Guid.NewGuid(), "Another Test", "description 2", 7, DateTimeOffset.UtcNow),
    //    new ItemDto(Guid.NewGuid(), "Another else Test", "description 3", 12, DateTimeOffset.UtcNow),
    //};

    [HttpGet]
    public async Task<IEnumerable<ItemDto>> GetAsync()
    {
        var items = (await ItemRepository.GetAllAsync())
                .Select(item => item.AsDto());
        return items;
    }
    
    [HttpGet, Route("{id:required}")]
    public async Task<ActionResult<ItemDto>> GetByIdAsync(Guid id)
    {
        var item = await ItemRepository.GetAsync(id);

        if (item == null)
        {
            return NotFound($"{id} wasn't found");
        }
        
        return Ok(item.AsDto());
    }

    [HttpPost]
    public async Task<ActionResult<Guid>> PostAsync(CreateItemDto itemDto)
    {
        var item = new Item
        {
            Name = itemDto.Name,
            Description = itemDto.Description,
            Price = itemDto.Price,
            DateCreated = DateTimeOffset.UtcNow
        };

        await ItemRepository.CreateAsync(item);

        return CreatedAtAction(nameof(GetByIdAsync), new {id = item.Id}, item.Id);
    }

    [HttpPut, Route("{id:required}")]
    public async Task<IActionResult> PutAsync(Guid id, UpdateItemDto itemDto)
    {
        var item = await ItemRepository.GetAsync(id);

        if (item == null)
            return NotFound();

        item.Name = itemDto.Name;
        item.Description = itemDto.Description;
        item.Price = itemDto.Price;

        await ItemRepository.UpdateAsync(item);
       
        return Ok();
    }

    [HttpDelete, Route("{id:required}")]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        var item = await ItemRepository.GetAsync(id);

        if (item == null)
            return NotFound();

        await ItemRepository.RemoveAsync(item.Id);

        return Ok();
    }
}

