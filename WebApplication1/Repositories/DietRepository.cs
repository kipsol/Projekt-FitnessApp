using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Repositories;

public class DietRepository : IDietRepository
{
    private readonly ApplicationDbContext _context;

    public DietRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IList<Diet>> GetAllAsync()
    {
        return await _context.Diets
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Diet?> GetByIdAsync(int id)
    {
        return await _context.Diets
            .AsNoTracking()
            .FirstOrDefaultAsync(d => d.Id == id);
    }

    public async Task AddAsync(Diet diet)
    {
        await _context.Diets.AddAsync(diet);
    }

    public async Task UpdateAsync(Diet diet)
    {
        _context.Attach(diet).State = EntityState.Modified;
    }

    public async Task DeleteAsync(int id)
    {
        var entity = await _context.Diets.FindAsync(id);
        if (entity is not null)
        {
            _context.Diets.Remove(entity);
        }
    }

    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }
}
