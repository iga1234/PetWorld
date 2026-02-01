using PetWorld.Domain.Enums;

namespace PetWorld.Domain.Entities;

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public ProductCategory Category { get; set; }
    public decimal Price { get; set; }
    public string Description { get; set; } = string.Empty;
    public string Brand { get; set; } = string.Empty;
}
