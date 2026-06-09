using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Repositories;

public class DietPlanDayRepository : IDietPlanDayRepository
{
    private readonly ApplicationDbContext _context;

    public DietPlanDayRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IList<DietPlanDay>> GetAllAsync()
    {
        return await _context.DietPlanDays
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<DietPlanDay?> GetByIdAsync(int id)
    {
        return await _context.DietPlanDays
            .AsNoTracking()
            .FirstOrDefaultAsync(d => d.Id == id);
    }

    public async Task<IList<Diet>> GetAllDietsAsync()
    {
        return await _context.Diets
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<IList<Meal>> GetAllMealsAsync()
    {
        return await _context.Meals
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task AddAsync(DietPlanDay dietPlanDay)
    {
        await _context.DietPlanDays.AddAsync(dietPlanDay);
    }

    public async Task UpdateAsync(DietPlanDay dietPlanDay)
    {
        _context.Attach(dietPlanDay).State = EntityState.Modified;
    }

    public async Task DeleteAsync(int id)
    {
        var entity = await _context.DietPlanDays.FindAsync(id);
        if (entity is not null)
        {
            _context.DietPlanDays.Remove(entity);
        }
    }

    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }
}
