using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication1.Models;
using WebApplication1.Repositories;
using WebApplication1.Services;

namespace WebApplication1.Pages.TrainingPlans;

public class DetailsModel : PageModel
{
    private readonly IPlanTreningowyRepository _repository;

    public DetailsModel(IPlanTreningowyRepository repository)
    {
        _repository = repository;
    }

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

    public async Task<IActionResult> OnGetPdfAsync(int? id)
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

        var pdf = TrainingPlanPdfGenerator.Generate(plan);
        var fileName = $"{SanitizeFileName(plan.Nazwa)}.pdf";

        return File(pdf, "application/pdf", fileName);
    }

    private static string SanitizeFileName(string value)
    {
        var invalidCharacters = Path.GetInvalidFileNameChars();
        var sanitized = new string(value
            .Select(character => invalidCharacters.Contains(character) ? '-' : character)
            .ToArray());

        return string.IsNullOrWhiteSpace(sanitized) ? "plan-treningowy" : sanitized;
    }
}
