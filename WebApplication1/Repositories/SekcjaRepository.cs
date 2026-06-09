using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Repositories;

public class SekcjaRepository : ISekcjaRepository
{
    private readonly ApplicationDbContext _context;

    public SekcjaRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IList<Sekcja>> GetAllAsync()
    {
        return await _context.Sekcje
            .Include(sekcja => sekcja.Maszyny)
            .OrderBy(sekcja => sekcja.Nazwa)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Sekcja?> GetByIdAsync(int id)
    {
        return await _context.Sekcje
            .AsNoTracking()
            .FirstOrDefaultAsync(sekcja => sekcja.Id == id);
    }

    public async Task<Sekcja?> GetByIdWithDetailsAsync(int id)
    {
        return await _context.Sekcje
            .Include(sekcja => sekcja.Maszyny)
            .FirstOrDefaultAsync(sekcja => sekcja.Id == id);
    }

    public async Task AddAsync(Sekcja sekcja)
    {
        await _context.Sekcje.AddAsync(sekcja);
    }

    public async Task UpdateAsync(Sekcja sekcja)
    {
        _context.Attach(sekcja).State = EntityState.Modified;
    }

    public async Task DeleteAsync(int id)
    {
        var sekcja = await _context.Sekcje.FindAsync(id);
        if (sekcja is not null)
        {
            _context.Sekcje.Remove(sekcja);
        }
    }

    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }
}
