<<<<<<< HEAD
using System.Security.Claims;
=======
>>>>>>> origin/master
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication1.Models;
using WebApplication1.Repositories;

namespace WebApplication1.Pages.ClassSchedulePages;

public class CreateModel : PageModel
{
    private readonly IClassScheduleRepository _repository;

    public CreateModel(IClassScheduleRepository repository)
    {
        _repository = repository;
    }

    public List<ClassEvent> DostepneZajecia { get; set; } = new();
    public List<int> JuzZapisanyId { get; set; } = new();

    public async Task OnGetAsync()
    {
<<<<<<< HEAD
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        DostepneZajecia = (await _repository.GetAllClassEventsAsync()).ToList();
        JuzZapisanyId = userId is null
            ? new List<int>()
            : (await _repository.GetEnrolledEventIdsAsync(userId)).ToList();
=======
        DostepneZajecia = (await _repository.GetAllClassEventsAsync()).ToList();
        JuzZapisanyId = (await _repository.GetEnrolledEventIdsAsync()).ToList();
>>>>>>> origin/master
    }

    public async Task<IActionResult> OnPostZapiszAsync(int classEventId)
    {
<<<<<<< HEAD
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId is null)
        {
            return Challenge();
        }

        var existing = await _repository.GetByClassEventAndUserAsync(classEventId, userId);
        if (existing is not null)
        {
            return RedirectToPage("./Index");
        }

        var nowyZapis = new ClassSchedule
        {
            ClassEventId = classEventId,
            UserId = userId
        };

=======
        var nowyZapis = new ClassSchedule { ClassEventId = classEventId };
>>>>>>> origin/master
        await _repository.AddAsync(nowyZapis);
        await _repository.SaveAsync();
        return RedirectToPage("./Index");
    }
}
