using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Repositories;

public class PartiaMiesniowaRepository : IPartiaMiesniowaRepository
{
    private readonly ApplicationDbContext _context;

    public PartiaMiesniowaRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IList<PartiaMiesniowa>> GetAllAsync()
    {
        return await _context.PartieMiesniowe
            .Include(partia => partia.Cwiczenia)
            .OrderBy(partia => partia.Nazwa)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<PartiaMiesniowa?> GetByIdAsync(int id)
    {
        return await _context.PartieMiesniowe
            .AsNoTracking()
            .FirstOrDefaultAsync(partia => partia.Id == id);
    }

    public async Task<PartiaMiesniowa?> GetByIdWithDetailsAsync(int id)
    {
        return await _context.PartieMiesniowe
            .Include(partia => partia.Cwiczenia)
            .FirstOrDefaultAsync(partia => partia.Id == id);
    }

    public async Task AddAsync(PartiaMiesniowa partiaMiesniowa)
    {
        await _context.PartieMiesniowe.AddAsync(partiaMiesniowa);
    }

    public async Task UpdateAsync(PartiaMiesniowa partiaMiesniowa)
    {
        _context.Attach(partiaMiesniowa).State = EntityState.Modified;
    }

    public async Task DeleteAsync(int id)
    {
        var partia = await _context.PartieMiesniowe.FindAsync(id);
        if (partia is not null)
        {
            _context.PartieMiesniowe.Remove(partia);
        }
    }

    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }
}
