
namespace Play.Catalog.Service;

public record ItemDto(Guid Id, string Name, string Description, decimal Price, DateTimeOffset DateCreated);

public record CreateItemDto
{
    public required string Name { get; set; }
    public required string Description { get; set; }
    public required decimal Price { get; set; }
}

public record UpdateItemDto
{
    public required string Name { get; set; }
    public required string Description { get; set; }
    public required decimal Price { get; set; }
};