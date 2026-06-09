using WebApplication1.Models;

namespace WebApplication1.Repositories;

public interface ICwiczenieRepository
{
    Task<IList<Cwiczenie>> GetAllAsync();
    Task<Cwiczenie?> GetByIdAsync(int id);
    Task<Cwiczenie?> GetByIdWithDetailsAsync(int id);
    Task<IList<PartiaMiesniowa>> GetAllPartieAsync();
    Task<IList<Maszyna>> GetAllMaszynyAsync();
    Task AddAsync(Cwiczenie cwiczenie);
    Task UpdateAsync(Cwiczenie cwiczenie);
    Task DeleteAsync(int id);
    Task SaveAsync();
}
