using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Repositories;

public class MealRepository : IMealRepository
{
    private readonly ApplicationDbContext _context;

    public MealRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IList<Meal>> GetAllAsync()
    {
        return await _context.Meals
            .Include(m => m.PlanDays)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Meal?> GetByIdAsync(int id)
    {
        return await _context.Meals
            .AsNoTracking()
            .FirstOrDefaultAsync(m => m.Id == id);
    }

    public async Task AddAsync(Meal meal)
    {
        await _context.Meals.AddAsync(meal);
    }

    public async Task UpdateAsync(Meal meal)
    {
        _context.Attach(meal).State = EntityState.Modified;
    }

    public async Task DeleteAsync(int id)
    {
        var entity = await _context.Meals.FindAsync(id);
        if (entity is not null)
        {
            _context.Meals.Remove(entity);
        }
    }

    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }
}
