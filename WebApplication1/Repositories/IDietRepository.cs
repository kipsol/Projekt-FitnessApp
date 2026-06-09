using WebApplication1.Models;

namespace WebApplication1.Repositories;

public interface IDietRepository
{
    Task<IList<Diet>> GetAllAsync();
    Task<Diet?> GetByIdAsync(int id);
    Task AddAsync(Diet diet);
    Task UpdateAsync(Diet diet);
    Task DeleteAsync(int id);
    Task SaveAsync();
}
