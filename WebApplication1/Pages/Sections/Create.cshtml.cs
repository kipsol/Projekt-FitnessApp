using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication1.Models;
using WebApplication1.Repositories;
using WebApplication1.DTOs;

namespace WebApplication1.Pages.Sections;

public class CreateModel : PageModel
{
    private readonly ISekcjaRepository _repository;

    public CreateModel(ISekcjaRepository repository)
    {
        _repository = repository;
    }

    [BindProperty]
    public SekcjaDto Sekcja { get; set; } = new();

    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        var entity = new Sekcja
        {
            Nazwa = Sekcja.Nazwa,
            Pietro = Sekcja.Pietro
        };

        await _repository.AddAsync(entity);
        await _repository.SaveAsync();
        return RedirectToPage("./Index");
    }
}
