using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly ApplicationDbContext _context;

    public ProductRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IList<Product>> GetAllAsync()
    {
        return await _context.Products
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<IList<Product>> GetAvailableAsync()
    {
        return await _context.Products
            .Where(p => p.Stock > 0)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Product?> GetByIdAsync(int id)
    {
        return await _context.Products
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task AddAsync(Product product)
    {
        await _context.Products.AddAsync(product);
    }

    public async Task UpdateAsync(Product product)
    {
        _context.Attach(product).State = EntityState.Modified;
    }

    public async Task DeleteAsync(int id)
    {
        var entity = await _context.Products.FindAsync(id);
        if (entity is not null)
        {
            _context.Products.Remove(entity);
        }
    }

    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }
}
