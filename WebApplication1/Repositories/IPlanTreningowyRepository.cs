using WebApplication1.Models;

namespace WebApplication1.Repositories;

public interface IPlanTreningowyRepository
{
    Task<IList<PlanTreningowy>> GetAllAsync();
    Task<PlanTreningowy?> GetByIdAsync(int id);
    Task<PlanTreningowy?> GetByIdWithDetailsAsync(int id);
    Task<IList<Cwiczenie>> GetAllCwiczeniaAsync();
    Task AddAsync(PlanTreningowy plan);
    Task UpdateAsync(PlanTreningowy plan);
    Task DeleteAsync(int id);
    Task AddPozycjaAsync(PozycjaPlanu pozycja);
    Task DeletePozycjaAsync(int pozycjaId);
    Task SaveAsync();
}
