using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication1.DTOs;
using WebApplication1.Models;
using WebApplication1.Repositories;

namespace WebApplication1.Pages.MealPages;

public class EditModel : PageModel
{
    private readonly IMealRepository _repository;

    public EditModel(IMealRepository repository)
    {
        _repository = repository;
    }

    [BindProperty]
    public MealDto Input { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id is null) return NotFound();

        var entity = await _repository.GetByIdAsync(id.Value);
        if (entity is null) return NotFound();

        Input = new MealDto
        {
            Id = entity.Id,
            Name = entity.Name,
            Calories = entity.Calories,
            Description = entity.Description
        };

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid) return Page();

        var entity = new Meal
        {
            Id = Input.Id,
            Name = Input.Name,
            Calories = Input.Calories,
            Description = Input.Description
        };

        await _repository.UpdateAsync(entity);
        await _repository.SaveAsync();
        return RedirectToPage("./Index");
    }
}
