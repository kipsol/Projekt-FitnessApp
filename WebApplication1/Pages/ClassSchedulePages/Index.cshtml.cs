using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Pages.ClassSchedulePages;

public class IndexModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public IndexModel(ApplicationDbContext context)
    {
        _context = context;
    }

    public Dictionary<DayOfWeek, List<ClassSchedule>> GrafikTygodniowy { get; set; } = new();

    public async Task OnGetAsync()
    {
        var zapisyZbazy = await _context.ClassSchedules
            .Include(cs => cs.ClassEvent)
            .ToListAsync();

        foreach (DayOfWeek dzien in Enum.GetValues(typeof(DayOfWeek)))
        {
            GrafikTygodniowy[dzien] = zapisyZbazy
                .Where(cs => cs.ClassEvent?.Day == dzien)
                .OrderBy(cs => cs.ClassEvent?.StartTime)
                .ToList();
        }
    }

    public async Task<IActionResult> OnPostDeleteFromScheduleAsync(int scheduleId)
    {
        var wpis = await _context.ClassSchedules.FindAsync(scheduleId);
        if (wpis != null)
        {
            _context.ClassSchedules.Remove(wpis);
            await _context.SaveChangesAsync();
        }

        return RedirectToPage();
    }
}