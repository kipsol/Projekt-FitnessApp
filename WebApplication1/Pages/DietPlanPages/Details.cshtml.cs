using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication1.Models;
using WebApplication1.Repositories;

namespace WebApplication1.Pages.DietPlanDayPages;

public class DetailsModel : PageModel
{
    private readonly IDietPlanDayRepository _repository;

    public DetailsModel(IDietPlanDayRepository repository)
    {
        _repository = repository;
    }

    public DietPlanDay DietPlanDay { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id is null) return NotFound();

        var entity = await _repository.GetByIdAsync(id.Value);
        if (entity is null) return NotFound();

        DietPlanDay = entity;
        return Page();
    }
}
