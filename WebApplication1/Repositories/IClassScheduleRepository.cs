using WebApplication1.Models;

namespace WebApplication1.Repositories;

public interface IClassScheduleRepository
{
    Task<IList<ClassSchedule>> GetAllAsync();
    Task<IList<ClassSchedule>> GetAllWithDetailsAsync();
    Task<IList<ClassSchedule>> GetAllWithDetailsAsync(string userId);
    Task<ClassSchedule?> GetByIdAsync(int id);
    Task<IList<ClassEvent>> GetAllClassEventsAsync();
    Task<IList<int>> GetEnrolledEventIdsAsync(string userId);
    Task<ClassSchedule?> GetByClassEventAndUserAsync(int classEventId, string userId);
    Task AddAsync(ClassSchedule classSchedule);
    Task UpdateAsync(ClassSchedule classSchedule);
    Task DeleteAsync(int id);
    Task SaveAsync();
}
