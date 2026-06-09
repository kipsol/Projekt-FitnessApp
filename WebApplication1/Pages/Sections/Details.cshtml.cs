using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication1.Models;
using WebApplication1.Repositories;

namespace WebApplication1.Pages.Sections;

public class DetailsModel : PageModel
{
    private readonly ISekcjaRepository _repository;

    public DetailsModel(ISekcjaRepository repository)
    {
        _repository = repository;
    }

    public Sekcja Sekcja { get; set; } = null!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        var sekcja = await _repository.GetByIdWithDetailsAsync(id.Value);

        if (sekcja is null)
        {
            return NotFound();
        }

        Sekcja = sekcja;
        return Page();
    }
}
