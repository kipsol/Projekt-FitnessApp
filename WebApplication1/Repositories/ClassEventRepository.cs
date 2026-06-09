using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Repositories;

public class ClassEventRepository : IClassEventRepository
{
    private readonly ApplicationDbContext _context;

    public ClassEventRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IList<ClassEvent>> GetAllAsync()
    {
        return await _context.ClassEvents
            .OrderBy(e => e.Day)
            .ThenBy(e => e.StartTime)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<ClassEvent?> GetByIdAsync(int id)
    {
        return await _context.ClassEvents
            .AsNoTracking()
            .FirstOrDefaultAsync(e => e.Id == id);
    }

    public async Task AddAsync(ClassEvent classEvent)
    {
        await _context.ClassEvents.AddAsync(classEvent);
    }

    public async Task UpdateAsync(ClassEvent classEvent)
    {
        _context.Attach(classEvent).State = EntityState.Modified;
    }

    public async Task DeleteAsync(int id)
    {
        var classEvent = await _context.ClassEvents.FindAsync(id);
        if (classEvent is not null)
        {
            _context.ClassEvents.Remove(classEvent);
        }
    }

    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }
}
