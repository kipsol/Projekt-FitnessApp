using WebApplication1.Models;

namespace WebApplication1.Repositories;

public interface IMealRepository
{
    Task<IList<Meal>> GetAllAsync();
    Task<Meal?> GetByIdAsync(int id);
    Task AddAsync(Meal meal);
    Task UpdateAsync(Meal meal);
    Task DeleteAsync(int id);
    Task SaveAsync();
}
