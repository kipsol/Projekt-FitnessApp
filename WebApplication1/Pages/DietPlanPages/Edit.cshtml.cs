using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication1.DTOs;
using WebApplication1.Models;
using WebApplication1.Repositories;

namespace WebApplication1.Pages.DietPlanDayPages;

public class EditModel : PageModel
{
    private readonly IDietPlanDayRepository _repository;

    public EditModel(IDietPlanDayRepository repository)
    {
        _repository = repository;
    }

    [BindProperty]
    public DietPlanDayDto Input { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id is null) return NotFound();

        var entity = await _repository.GetByIdAsync(id.Value);
        if (entity is null) return NotFound();

        Input = new DietPlanDayDto
        {
            Id = entity.Id,
            DietId = entity.DietId,
            MealId = entity.MealId,
            Day = entity.Day,
            MealType = entity.MealType
        };

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid) return Page();

        var entity = new DietPlanDay
        {
            Id = Input.Id,
            DietId = Input.DietId,
            MealId = Input.MealId,
            Day = Input.Day,
            MealType = Input.MealType
        };

        await _repository.UpdateAsync(entity);
        await _repository.SaveAsync();
        return RedirectToPage("./Index");
    }
}
