using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication1.Models;
using WebApplication1.Repositories;

namespace WebApplication1.Pages.Sections;

public class DeleteModel : PageModel
{
    private readonly ISekcjaRepository _repository;

    public DeleteModel(ISekcjaRepository repository)
    {
        _repository = repository;
    }

    [BindProperty]
    public Sekcja Sekcja { get; set; } = null!;

    public bool HasMaszyny { get; set; }

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
        HasMaszyny = sekcja.Maszyny.Any();
        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        var sekcja = await _repository.GetByIdWithDetailsAsync(id.Value);

        if (sekcja is null)
        {
            return RedirectToPage("./Index");
        }

        if (sekcja.Maszyny.Any())
        {
            Sekcja = sekcja;
            HasMaszyny = true;
            ModelState.AddModelError(string.Empty, "Nie mozna usunac sekcji, ktora ma przypisane maszyny.");
            return Page();
        }

        await _repository.DeleteAsync(sekcja.Id);
        await _repository.SaveAsync();

        return RedirectToPage("./Index");
    }
}
