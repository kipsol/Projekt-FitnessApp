using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication1.Models;
using WebApplication1.Repositories;

namespace WebApplication1.Pages.ClassSchedulePages;

public class DetailsModel : PageModel
{
    private readonly IClassScheduleRepository _repository;

    public DetailsModel(IClassScheduleRepository repository)
    {
        _repository = repository;
    }

    public ClassSchedule ClassSchedule { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id is null) return NotFound();

        var entity = await _repository.GetByIdAsync(id.Value);
        if (entity is null) return NotFound();

        ClassSchedule = entity;
        return Page();
    }
}
