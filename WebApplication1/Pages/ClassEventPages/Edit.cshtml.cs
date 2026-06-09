using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication1.DTOs;
using WebApplication1.Models;
using WebApplication1.Repositories;

namespace WebApplication1.Pages.ClassEventPages;

public class EditModel : PageModel
{
    private readonly IClassEventRepository _repository;

    public EditModel(IClassEventRepository repository)
    {
        _repository = repository;
    }

    [BindProperty]
    public ClassEventDto Input { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        var entity = await _repository.GetByIdAsync(id.Value);
        if (entity is null)
        {
            return NotFound();
        }

        Input = new ClassEventDto
        {
            Id = entity.Id,
            Name = entity.Name,
            Day = entity.Day,
            Trainer = entity.Trainer,
            StartTime = entity.StartTime,
            Duration = entity.Duration,
            Description = entity.Description
        };

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        var entity = new ClassEvent
        {
            Id = Input.Id,
            Name = Input.Name,
            Day = Input.Day,
            Trainer = Input.Trainer,
            StartTime = Input.StartTime,
            Duration = Input.Duration,
            Description = Input.Description
        };

        await _repository.UpdateAsync(entity);
        await _repository.SaveAsync();
        return RedirectToPage("./Index");
    }
}
