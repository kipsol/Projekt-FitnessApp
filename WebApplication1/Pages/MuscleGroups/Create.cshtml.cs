using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication1.Models;
using WebApplication1.Repositories;
using WebApplication1.DTOs;

namespace WebApplication1.Pages.MuscleGroups;

public class CreateModel : PageModel
{
    private readonly IPartiaMiesniowaRepository _repository;

    public CreateModel(IPartiaMiesniowaRepository repository)
    {
        _repository = repository;
    }

    [BindProperty]
    public PartiaMiesniowaDto Partia { get; set; } = new();

    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        var entity = new PartiaMiesniowa
        {
            Nazwa = Partia.Nazwa
        };

        await _repository.AddAsync(entity);
        await _repository.SaveAsync();

        return RedirectToPage("./Index");
    }
}
