using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication1.Models;
using WebApplication1.Repositories;
using WebApplication1.Security;

namespace WebApplication1.Pages.ClassSchedulePages;

public class IndexModel : PageModel
{
    private readonly IClassScheduleRepository _repository;

    public IndexModel(IClassScheduleRepository repository)
    {
        _repository = repository;
    }

    public Dictionary<DayOfWeek, List<ClassSchedule>> GrafikTygodniowy { get; set; } = new();

    public bool CanManageClasses => User.HasClaim(AppClaimTypes.Permission, AppPermissions.ManageFitnessClasses);

    public async Task OnGetAsync()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var zapisy = CanManageClasses
            ? await _repository.GetAllWithDetailsAsync()
            : userId is null
                ? new List<ClassSchedule>()
                : await _repository.GetAllWithDetailsAsync(userId);

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
        var zapis = await _repository.GetByIdAsync(scheduleId);

        if (zapis is null)
        {
            return RedirectToPage();
        }

        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (!CanManageClasses && zapis.UserId != userId)
        {
            return Forbid();
        }

        await _repository.DeleteAsync(scheduleId);
        await _repository.SaveAsync();
        return RedirectToPage();
    }
}
