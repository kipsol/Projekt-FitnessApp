using WebApplication1.Models;

namespace WebApplication1.Repositories;

public interface ISekcjaRepository
{
    Task<IList<Sekcja>> GetAllAsync();
    Task<Sekcja?> GetByIdAsync(int id);
    Task<Sekcja?> GetByIdWithDetailsAsync(int id);
    Task AddAsync(Sekcja sekcja);
    Task UpdateAsync(Sekcja sekcja);
    Task DeleteAsync(int id);
    Task SaveAsync();
}
