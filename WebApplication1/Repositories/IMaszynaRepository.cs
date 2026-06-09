using WebApplication1.Models;

namespace WebApplication1.Repositories;

public interface IMaszynaRepository
{
    Task<IList<Maszyna>> GetAllAsync();
    Task<Maszyna?> GetByIdAsync(int id);
    Task<Maszyna?> GetByIdWithDetailsAsync(int id);
    Task<IList<Sekcja>> GetAllSekcjeAsync();
    Task AddAsync(Maszyna maszyna);
    Task UpdateAsync(Maszyna maszyna);
    Task DeleteAsync(int id);
    Task SaveAsync();
}
