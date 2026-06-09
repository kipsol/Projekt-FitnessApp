using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication1.DTOs;
using WebApplication1.Models;
using WebApplication1.Repositories;

namespace WebApplication1.Pages.Sections;

public class EditModel : PageModel
{
    private readonly ISekcjaRepository _repository;

    public EditModel(ISekcjaRepository repository)
    {
        _repository = repository;
    }

    [BindProperty]
    public SekcjaDto Sekcja { get; set; } = null!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        var sekcja = await _repository.GetByIdAsync(id.Value);

        if (sekcja is null)
        {
            return NotFound();
        }

        Sekcja = new SekcjaDto
        {
            Id = sekcja.Id,
            Nazwa = sekcja.Nazwa,
            Pietro = sekcja.Pietro
        };
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        var entity = new Sekcja
        {
            Id = Sekcja.Id,
            Nazwa = Sekcja.Nazwa,
            Pietro = Sekcja.Pietro
        };

        await _repository.UpdateAsync(entity);
        await _repository.SaveAsync();

        return RedirectToPage("./Index");
    }
}
