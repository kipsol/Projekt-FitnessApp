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
        DostepneZajecia = (await _repository.GetAllClassEventsAsync()).ToList();
        JuzZapisanyId = (await _repository.GetEnrolledEventIdsAsync()).ToList();
    }

    public async Task<IActionResult> OnPostZapiszAsync(int classEventId)
    {
        var nowyZapis = new ClassSchedule { ClassEventId = classEventId };
        await _repository.AddAsync(nowyZapis);
        await _repository.SaveAsync();
        return RedirectToPage("./Index");
    }
}
