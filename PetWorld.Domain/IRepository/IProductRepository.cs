using PetWorld.Domain.Entities;

namespace PetWorld.Domain.IRepository;

public interface IProductRepository
{
    Task<IEnumerable<Product>> GetAllAsync();
    Task<Product?> GetByIdAsync(int id);
    Task<IEnumerable<Product>> GetByCategoryAsync(string category);
    Task<IEnumerable<Product>> SearchAsync(string searchTerm);
}
