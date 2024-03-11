using Microsoft.AspNetCore.Mvc;

namespace Play.Catalog.Service.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ItemsController : ControllerBase
{
    //mock
    private static readonly List<ItemDto> Items = new()
    {
        new ItemDto(Guid.NewGuid(), "Test", "description", 5, DateTimeOffset.UtcNow),
        new ItemDto(Guid.NewGuid(), "Another Test", "description 2", 7, DateTimeOffset.UtcNow),
        new ItemDto(Guid.NewGuid(), "Another else Test", "description 3", 12, DateTimeOffset.UtcNow),
    };

    [HttpGet]
    public IEnumerable<ItemDto> Get()
    {
        return Items;
    }
    
    [HttpGet, Route("{id:required}")]
    public ActionResult<ItemDto> GetById(Guid id)
    {
        var item =  Items.Find(i => i.Id == id);

        if (item == null)
        {
            return NotFound($"{id} wasn't found");
        }
        
        return Ok(item);
    }

    [HttpPost]
    public ActionResult<Guid> Post(CreateItemDto itemDto)
    {
        var item = new ItemDto(Guid.NewGuid(), itemDto.Name, itemDto.Description, itemDto.Price, DateTimeOffset.UtcNow);
        Items.Add(item);

        return CreatedAtAction(nameof(GetById), new {id = item.Id}, item.Id);
    }

    [HttpPut, Route("{id:required}")]
    public IActionResult Put(Guid id, UpdateItemDto itemDto)
    {
        var item = Items.Find(i => i.Id == id);

        if (item == null)
        {
            return NotFound($"{id} wasn't found");
        }

        var updatedItem = item with
        {
            Name = itemDto.Name,
            Description = itemDto.Description,
            Price = itemDto.Price,
        };

        var index = Items.FindIndex(i => i.Id == id);
        Items[index] = updatedItem;

        return Ok();
    }

    [HttpDelete, Route("{id:required}")]
    public IActionResult Delete(Guid id)
    {
        var item = Items.Find(i => i.Id == id);

        if (item == null)
        {
            return NotFound($"{id} wasn't found");
        }

        Items.Remove(item);

        return Ok();
    }
}

