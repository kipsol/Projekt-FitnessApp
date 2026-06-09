using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Repositories;

public class MaszynaRepository : IMaszynaRepository
{
    private readonly ApplicationDbContext _context;

    public MaszynaRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IList<Maszyna>> GetAllAsync()
    {
        return await _context.Maszyny
            .Include(maszyna => maszyna.Sekcja)
            .Include(maszyna => maszyna.Cwiczenia)
            .OrderBy(maszyna => maszyna.Nazwa)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Maszyna?> GetByIdAsync(int id)
    {
        return await _context.Maszyny
            .AsNoTracking()
            .FirstOrDefaultAsync(maszyna => maszyna.Id == id);
    }

    public async Task<Maszyna?> GetByIdWithDetailsAsync(int id)
    {
        return await _context.Maszyny
            .Include(maszyna => maszyna.Sekcja)
            .Include(maszyna => maszyna.Cwiczenia)
            .FirstOrDefaultAsync(maszyna => maszyna.Id == id);
    }

    public async Task<IList<Sekcja>> GetAllSekcjeAsync()
    {
        return await _context.Sekcje
            .OrderBy(sekcja => sekcja.Nazwa)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task AddAsync(Maszyna maszyna)
    {
        await _context.Maszyny.AddAsync(maszyna);
    }

    public async Task UpdateAsync(Maszyna maszyna)
    {
        _context.Attach(maszyna).State = EntityState.Modified;
    }

    public async Task DeleteAsync(int id)
    {
        var maszyna = await _context.Maszyny.FindAsync(id);
        if (maszyna is not null)
        {
            _context.Maszyny.Remove(maszyna);
        }
    }

    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }
}
