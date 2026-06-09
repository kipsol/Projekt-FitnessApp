using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication1.Models;
using WebApplication1.Repositories;

namespace WebApplication1.Pages.Exercises;

public class DetailsModel : PageModel
{
    private readonly ICwiczenieRepository _repository;

    public DetailsModel(ICwiczenieRepository repository)
    {
        _repository = repository;
    }

    public Cwiczenie Cwiczenie { get; set; } = null!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        var cwiczenie = await _repository.GetByIdWithDetailsAsync(id.Value);

        if (cwiczenie is null)
        {
            return NotFound();
        }

        Cwiczenie = cwiczenie;
        return Page();
    }
}
