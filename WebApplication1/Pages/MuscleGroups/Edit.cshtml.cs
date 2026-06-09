using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication1.DTOs;
using WebApplication1.Models;
using WebApplication1.Repositories;

namespace WebApplication1.Pages.MuscleGroups;

public class EditModel : PageModel
{
    private readonly IPartiaMiesniowaRepository _repository;

    public EditModel(IPartiaMiesniowaRepository repository)
    {
        _repository = repository;
    }

    [BindProperty]
    public PartiaMiesniowaDto PartiaMiesniowa { get; set; } = null!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        var partia = await _repository.GetByIdAsync(id.Value);

        if (partia is null)
        {
            return NotFound();
        }

        PartiaMiesniowa = new PartiaMiesniowaDto
        {
            Id = partia.Id,
            Nazwa = partia.Nazwa
        };

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        var entity = new PartiaMiesniowa
        {
            Id = PartiaMiesniowa.Id,
            Nazwa = PartiaMiesniowa.Nazwa
        };

        await _repository.UpdateAsync(entity);
        await _repository.SaveAsync();

        return RedirectToPage("./Index");
    }
}
