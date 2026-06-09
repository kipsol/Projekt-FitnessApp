using WebApplication1.Models;

namespace WebApplication1.Repositories;

public interface IPartiaMiesniowaRepository
{
    Task<IList<PartiaMiesniowa>> GetAllAsync();
    Task<PartiaMiesniowa?> GetByIdAsync(int id);
    Task<PartiaMiesniowa?> GetByIdWithDetailsAsync(int id);
    Task AddAsync(PartiaMiesniowa partiaMiesniowa);
    Task UpdateAsync(PartiaMiesniowa partiaMiesniowa);
    Task DeleteAsync(int id);
    Task SaveAsync();
}
