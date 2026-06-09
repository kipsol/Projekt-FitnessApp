using WebApplication1.Models;

namespace WebApplication1.Repositories;

public interface IOrderItemRepository
{
    Task<IList<OrderItem>> GetAllWithDetailsAsync();
    Task<OrderItem?> GetByIdAsync(int id);
    Task<IList<Order>> GetAllOrdersAsync();
    Task<IList<Product>> GetAvailableProductsAsync();
    Task<Product?> GetProductByIdAsync(int id);
    Task<Order?> GetOrderByIdAsync(int id);
    Task AddAsync(OrderItem orderItem);
    Task UpdateAsync(OrderItem orderItem);
    Task DeleteAsync(int id);
    Task SaveAsync();
}
