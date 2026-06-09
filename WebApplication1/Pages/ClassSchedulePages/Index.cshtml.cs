using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication1.Models;
using WebApplication1.Repositories;

namespace WebApplication1.Pages.ClassSchedulePages;

public class IndexModel : PageModel
{
    private readonly IClassScheduleRepository _repository;

    public IndexModel(IClassScheduleRepository repository)
    {
        _repository = repository;
    }

    public Dictionary<DayOfWeek, List<ClassSchedule>> GrafikTygodniowy { get; set; } = new();

    public async Task OnGetAsync()
    {
        var zapisy = await _repository.GetAllWithDetailsAsync();

        foreach (DayOfWeek dzien in Enum.GetValues(typeof(DayOfWeek)))
        {
            GrafikTygodniowy[dzien] = zapisy
                .Where(cs => cs.ClassEvent?.Day == dzien)
                .OrderBy(cs => cs.ClassEvent?.StartTime)
                .ToList();
        }
    }

    public async Task<IActionResult> OnPostDeleteFromScheduleAsync(int scheduleId)
    {
        await _repository.DeleteAsync(scheduleId);
        await _repository.SaveAsync();
        return RedirectToPage();
    }
}
