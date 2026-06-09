using WebApplication1.Models;

namespace WebApplication1.Repositories;

public interface IClassEventRepository
{
    Task<IList<ClassEvent>> GetAllAsync();
    Task<ClassEvent?> GetByIdAsync(int id);
    Task AddAsync(ClassEvent classEvent);
    Task UpdateAsync(ClassEvent classEvent);
    Task DeleteAsync(int id);
    Task SaveAsync();
}
