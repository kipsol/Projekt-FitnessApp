using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication1.Models;
using WebApplication1.Repositories;

namespace WebApplication1.Pages.MuscleGroups;

public class DetailsModel : PageModel
{
    private readonly IPartiaMiesniowaRepository _repository;

    public DetailsModel(IPartiaMiesniowaRepository repository)
    {
        _repository = repository;
    }

    public PartiaMiesniowa PartiaMiesniowa { get; set; } = null!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        var partia = await _repository.GetByIdWithDetailsAsync(id.Value);

        if (partia is null)
        {
            return NotFound();
        }

        PartiaMiesniowa = partia;
        return Page();
    }
}
