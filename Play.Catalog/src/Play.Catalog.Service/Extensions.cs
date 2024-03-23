using Play.Catalog.Service.Entities;

namespace Play.Catalog.Service;

public static class Extensions
{
    public static ItemDto AsDto(this Item item) =>
        new(item.Id, item.Name, item.Description, item.Price, item.DateCreated);

    public static Item FromDto(this ItemDto itemDto)
    {
        var item = new Item
        {
            Id = itemDto.Id,
            Name = itemDto.Name,
            Description = itemDto.Description,
            Price = itemDto.Price,
            DateCreated = itemDto.DateCreated
        };

        return item;
    }

}

