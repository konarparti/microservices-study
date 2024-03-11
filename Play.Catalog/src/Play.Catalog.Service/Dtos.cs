
using System.ComponentModel.DataAnnotations;

namespace Play.Catalog.Service;

public record ItemDto(Guid Id, string Name, string Description, decimal Price, DateTimeOffset DateCreated);

public record CreateItemDto
{
    [Required]
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
};

public record UpdateItemDto
{
    [Required]
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
};