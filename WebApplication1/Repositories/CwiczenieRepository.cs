using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Repositories;

public class CwiczenieRepository : ICwiczenieRepository
{
    private readonly ApplicationDbContext _context;

    public CwiczenieRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IList<Cwiczenie>> GetAllAsync()
    {
        return await _context.Cwiczenia
            .Include(cwiczenie => cwiczenie.PartiaMiesniowa)
            .Include(cwiczenie => cwiczenie.Maszyna)
            .Include(cwiczenie => cwiczenie.PozycjePlanu)
                .ThenInclude(pozycja => pozycja.PlanTreningowy)
            .OrderBy(cwiczenie => cwiczenie.Nazwa)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Cwiczenie?> GetByIdAsync(int id)
    {
        return await _context.Cwiczenia
            .AsNoTracking()
            .FirstOrDefaultAsync(cwiczenie => cwiczenie.Id == id);
    }

    public async Task<Cwiczenie?> GetByIdWithDetailsAsync(int id)
    {
        return await _context.Cwiczenia
            .Include(cwiczenie => cwiczenie.PartiaMiesniowa)
            .Include(cwiczenie => cwiczenie.Maszyna)
            .Include(cwiczenie => cwiczenie.PozycjePlanu)
                .ThenInclude(pozycja => pozycja.PlanTreningowy)
            .FirstOrDefaultAsync(cwiczenie => cwiczenie.Id == id);
    }

    public async Task<IList<PartiaMiesniowa>> GetAllPartieAsync()
    {
        return await _context.PartieMiesniowe
            .OrderBy(partia => partia.Nazwa)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<IList<Maszyna>> GetAllMaszynyAsync()
    {
        return await _context.Maszyny
            .OrderBy(maszyna => maszyna.Nazwa)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task AddAsync(Cwiczenie cwiczenie)
    {
        await _context.Cwiczenia.AddAsync(cwiczenie);
    }

    public async Task UpdateAsync(Cwiczenie cwiczenie)
    {
        _context.Attach(cwiczenie).State = EntityState.Modified;
    }

    public async Task DeleteAsync(int id)
    {
        var cwiczenie = await _context.Cwiczenia.FindAsync(id);
        if (cwiczenie is not null)
        {
            _context.Cwiczenia.Remove(cwiczenie);
        }
    }

    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }
}
