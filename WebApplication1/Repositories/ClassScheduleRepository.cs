using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Repositories;

public class ClassScheduleRepository : IClassScheduleRepository
{
    private readonly ApplicationDbContext _context;

    public ClassScheduleRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IList<ClassSchedule>> GetAllAsync()
    {
        return await _context.ClassSchedules
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<IList<ClassSchedule>> GetAllWithDetailsAsync()
    {
        return await _context.ClassSchedules
            .Include(cs => cs.ClassEvent)
<<<<<<< HEAD
            .Include(cs => cs.User)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<IList<ClassSchedule>> GetAllWithDetailsAsync(string userId)
    {
        return await _context.ClassSchedules
            .Include(cs => cs.ClassEvent)
            .Where(cs => cs.UserId == userId)
=======
>>>>>>> origin/master
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<ClassSchedule?> GetByIdAsync(int id)
    {
        return await _context.ClassSchedules
            .AsNoTracking()
            .FirstOrDefaultAsync(cs => cs.Id == id);
    }

    public async Task<IList<ClassEvent>> GetAllClassEventsAsync()
    {
        return await _context.ClassEvents
            .OrderBy(c => c.Day)
            .ThenBy(c => c.StartTime)
            .AsNoTracking()
            .ToListAsync();
    }

<<<<<<< HEAD
    public async Task<IList<int>> GetEnrolledEventIdsAsync(string userId)
    {
        return await _context.ClassSchedules
            .Where(cs => cs.UserId == userId)
=======
    public async Task<IList<int>> GetEnrolledEventIdsAsync()
    {
        return await _context.ClassSchedules
>>>>>>> origin/master
            .Select(cs => cs.ClassEventId)
            .ToListAsync();
    }

<<<<<<< HEAD
    public async Task<ClassSchedule?> GetByClassEventAndUserAsync(int classEventId, string userId)
    {
        return await _context.ClassSchedules
            .FirstOrDefaultAsync(cs => cs.ClassEventId == classEventId && cs.UserId == userId);
    }

=======
>>>>>>> origin/master
    public async Task AddAsync(ClassSchedule classSchedule)
    {
        await _context.ClassSchedules.AddAsync(classSchedule);
    }

    public async Task UpdateAsync(ClassSchedule classSchedule)
    {
        _context.Attach(classSchedule).State = EntityState.Modified;
    }

    public async Task DeleteAsync(int id)
    {
        var entity = await _context.ClassSchedules.FindAsync(id);
        if (entity is not null)
        {
            _context.ClassSchedules.Remove(entity);
        }
    }

    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }
}
