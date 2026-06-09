using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication1.DTOs;
using WebApplication1.Models;
using WebApplication1.Repositories;

namespace WebApplication1.Pages.ClassSchedulePages;

public class EditModel : PageModel
{
    private readonly IClassScheduleRepository _repository;

    public EditModel(IClassScheduleRepository repository)
    {
        _repository = repository;
    }

    [BindProperty]
    public ClassScheduleDto Input { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id is null) return NotFound();

        var entity = await _repository.GetByIdAsync(id.Value);
        if (entity is null) return NotFound();

        Input = new ClassScheduleDto { Id = entity.Id, ClassEventId = entity.ClassEventId };
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid) return Page();

        var entity = new ClassSchedule { Id = Input.Id, ClassEventId = Input.ClassEventId };
        await _repository.UpdateAsync(entity);
        await _repository.SaveAsync();
        return RedirectToPage("./Index");
    }
}
