using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Repositories;

public class OrderItemRepository : IOrderItemRepository
{
    private readonly ApplicationDbContext _context;

    public OrderItemRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IList<OrderItem>> GetAllWithDetailsAsync()
    {
        return await _context.OrderItems
            .Include(o => o.Order)
            .Include(o => o.Product)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<OrderItem?> GetByIdAsync(int id)
    {
        return await _context.OrderItems
            .AsNoTracking()
            .FirstOrDefaultAsync(o => o.Id == id);
    }

    public async Task<IList<Order>> GetAllOrdersAsync()
    {
        return await _context.Orders
            .OrderByDescending(o => o.Id)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<IList<Product>> GetAvailableProductsAsync()
    {
        return await _context.Products
            .Where(p => p.Stock > 0)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Product?> GetProductByIdAsync(int id)
    {
        return await _context.Products.FindAsync(id);
    }

    public async Task<Order?> GetOrderByIdAsync(int id)
    {
        return await _context.Orders.FindAsync(id);
    }

    public async Task AddAsync(OrderItem orderItem)
    {
        await _context.OrderItems.AddAsync(orderItem);
    }

    public async Task UpdateAsync(OrderItem orderItem)
    {
        _context.Attach(orderItem).State = EntityState.Modified;
    }

    public async Task DeleteAsync(int id)
    {
        var entity = await _context.OrderItems.FindAsync(id);
        if (entity is not null)
        {
            _context.OrderItems.Remove(entity);
        }
    }

    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }
}
