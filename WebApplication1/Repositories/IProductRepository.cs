using WebApplication1.Models;

namespace WebApplication1.Repositories;

public interface IProductRepository
{
    Task<IList<Product>> GetAllAsync();
    Task<IList<Product>> GetAvailableAsync();
    Task<Product?> GetByIdAsync(int id);
    Task AddAsync(Product product);
    Task UpdateAsync(Product product);
    Task DeleteAsync(int id);
    Task SaveAsync();
}
