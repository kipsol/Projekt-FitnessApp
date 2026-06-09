using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication1.Models;
using WebApplication1.Repositories;

namespace WebApplication1.Pages.TrainingPlans;

public class DeleteModel : PageModel
{
    private readonly IPlanTreningowyRepository _repository;

    public DeleteModel(IPlanTreningowyRepository repository)
    {
        _repository = repository;
    }

    [BindProperty]
    public PlanTreningowy PlanTreningowy { get; set; } = null!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        var plan = await _repository.GetByIdWithDetailsAsync(id.Value);

        if (plan is null)
        {
            return NotFound();
        }

        PlanTreningowy = plan;
        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        await _repository.DeleteAsync(id.Value);
        await _repository.SaveAsync();

        return RedirectToPage("./Index");
    }
}
