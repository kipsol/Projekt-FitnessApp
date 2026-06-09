using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApplication1.Models;
using WebApplication1.Repositories;
using WebApplication1.DTOs;

namespace WebApplication1.Pages.GymMachines;

public class CreateModel : PageModel
{
    private readonly IMaszynaRepository _repository;

    public CreateModel(IMaszynaRepository repository)
    {
        _repository = repository;
    }

    [BindProperty]
    public MaszynaDto Maszyna { get; set; } = new();

    public SelectList Sekcje { get; set; } = null!;

    public async Task OnGetAsync()
    {
        await LoadSekcjeAsync();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            await LoadSekcjeAsync();
            return Page();
        }

        var entity = new Maszyna
        {
            Nazwa = Maszyna.Nazwa,
            Status = Maszyna.Status,
            SekcjaId = Maszyna.SekcjaId
        };

        await _repository.AddAsync(entity);
        await _repository.SaveAsync();
        return RedirectToPage("./Index");
    }

    private async Task LoadSekcjeAsync()
    {
        var sekcje = await _repository.GetAllSekcjeAsync();
        Sekcje = new SelectList(sekcje, "Id", "Nazwa");
    }
}
