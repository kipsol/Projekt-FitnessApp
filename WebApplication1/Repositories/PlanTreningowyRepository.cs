using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Repositories;

public class PlanTreningowyRepository : IPlanTreningowyRepository
{
    private readonly ApplicationDbContext _context;

    public PlanTreningowyRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IList<PlanTreningowy>> GetAllAsync()
    {
        return await _context.PlanyTreningowe
            .Include(plan => plan.PozycjePlanu)
                .ThenInclude(pozycja => pozycja.Cwiczenie)
            .OrderBy(plan => plan.Nazwa)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<PlanTreningowy?> GetByIdAsync(int id)
    {
        return await _context.PlanyTreningowe
            .AsNoTracking()
            .FirstOrDefaultAsync(plan => plan.Id == id);
    }

    public async Task<PlanTreningowy?> GetByIdWithDetailsAsync(int id)
    {
        return await _context.PlanyTreningowe
            .Include(plan => plan.PozycjePlanu)
                .ThenInclude(pozycja => pozycja.Cwiczenie)
            .FirstOrDefaultAsync(plan => plan.Id == id);
    }

    public async Task<IList<Cwiczenie>> GetAllCwiczeniaAsync()
    {
        return await _context.Cwiczenia
            .OrderBy(cwiczenie => cwiczenie.Nazwa)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task AddAsync(PlanTreningowy plan)
    {
        await _context.PlanyTreningowe.AddAsync(plan);
    }

    public async Task UpdateAsync(PlanTreningowy plan)
    {
        _context.Attach(plan).State = EntityState.Modified;
    }

    public async Task DeleteAsync(int id)
    {
        var plan = await _context.PlanyTreningowe.FindAsync(id);
        if (plan is not null)
        {
            _context.PlanyTreningowe.Remove(plan);
        }
    }

    public async Task AddPozycjaAsync(PozycjaPlanu pozycja)
    {
        await _context.PozycjePlanu.AddAsync(pozycja);
    }

    public async Task DeletePozycjaAsync(int pozycjaId)
    {
        var pozycja = await _context.PozycjePlanu.FindAsync(pozycjaId);
        if (pozycja is not null)
        {
            _context.PozycjePlanu.Remove(pozycja);
        }
    }

    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }
}
