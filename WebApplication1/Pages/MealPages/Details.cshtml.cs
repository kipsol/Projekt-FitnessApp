using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication1.Models;
using WebApplication1.Repositories;

namespace WebApplication1.Pages.MealPages;

public class DetailsModel : PageModel
{
    private readonly IMealRepository _repository;

    public DetailsModel(IMealRepository repository)
    {
        _repository = repository;
    }

    public Meal Meal { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id is null) return NotFound();

        var entity = await _repository.GetByIdAsync(id.Value);
        if (entity is null) return NotFound();

        Meal = entity;
        return Page();
    }
}
