using PetWorld.Domain.Entities;
using PetWorld.Domain.Enums;

namespace PetWorld.Domain.Interfaces;

public interface IProductRepository
{
    Task<IEnumerable<Product>> GetAllAsync();
    Task<Product?> GetByIdAsync(int id);
    Task<IEnumerable<Product>> GetByCategoryAsync(ProductCategory category);
    Task<IEnumerable<Product>> SearchAsync(string searchTerm);
}
