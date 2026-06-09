using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication1.Models;
using WebApplication1.Repositories;

namespace WebApplication1.Pages.MuscleGroups;

public class DeleteModel : PageModel
{
    private readonly IPartiaMiesniowaRepository _repository;

    public DeleteModel(IPartiaMiesniowaRepository repository)
    {
        _repository = repository;
    }

    [BindProperty]
    public PartiaMiesniowa PartiaMiesniowa { get; set; } = null!;

    public bool HasCwiczenia { get; set; }

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
        HasCwiczenia = partia.Cwiczenia.Any();
        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        var partia = await _repository.GetByIdWithDetailsAsync(id.Value);

        if (partia is null)
        {
            return RedirectToPage("./Index");
        }

        if (partia.Cwiczenia.Any())
        {
            PartiaMiesniowa = partia;
            HasCwiczenia = true;
            ModelState.AddModelError(string.Empty, "Nie mozna usunac partii miesniowej, ktora ma przypisane cwiczenia.");
            return Page();
        }

        await _repository.DeleteAsync(partia.Id);
        await _repository.SaveAsync();

        return RedirectToPage("./Index");
    }
}
