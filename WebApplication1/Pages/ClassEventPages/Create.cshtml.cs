using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication1.DTOs;
using WebApplication1.Models;
using WebApplication1.Repositories;

namespace WebApplication1.Pages.ClassEventPages;

public class CreateModel : PageModel
{
    private readonly IClassEventRepository _repository;

    public CreateModel(IClassEventRepository repository)
    {
        _repository = repository;
    }

    public IActionResult OnGet()
    {
        return Page();
    }

    [BindProperty]
    public ClassEventDto Input { get; set; } = default!;

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        var entity = new ClassEvent
        {
            Name = Input.Name,
            Day = Input.Day,
            Trainer = Input.Trainer,
            StartTime = Input.StartTime,
            Duration = Input.Duration,
            Description = Input.Description
        };

        await _repository.AddAsync(entity);
        await _repository.SaveAsync();
        return RedirectToPage("/ClassSchedulePages/Index");
    }
}
