using WebApplication1.Models;

namespace WebApplication1.Repositories;

public interface IOrderRepository
{
    Task<IList<Order>> GetAllAsync();
    Task<IList<Order>> GetAllByUserIdAsync(string userId);
    Task<Order?> GetByIdAsync(int id);
    Task AddAsync(Order order);
    Task UpdateAsync(Order order);
    Task DeleteAsync(int id);
    Task SaveAsync();
}
