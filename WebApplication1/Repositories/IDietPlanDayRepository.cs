using WebApplication1.Models;

namespace WebApplication1.Repositories;

public interface IDietPlanDayRepository
{
    Task<IList<DietPlanDay>> GetAllAsync();
    Task<DietPlanDay?> GetByIdAsync(int id);
    Task<IList<Diet>> GetAllDietsAsync();
    Task<IList<Meal>> GetAllMealsAsync();
    Task AddAsync(DietPlanDay dietPlanDay);
    Task UpdateAsync(DietPlanDay dietPlanDay);
    Task DeleteAsync(int id);
    Task SaveAsync();
}
