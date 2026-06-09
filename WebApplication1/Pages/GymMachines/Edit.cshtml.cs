using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApplication1.DTOs;
using WebApplication1.Models;
using WebApplication1.Repositories;

namespace WebApplication1.Pages.GymMachines;

public class EditModel : PageModel
{
    private readonly IMaszynaRepository _repository;

    public EditModel(IMaszynaRepository repository)
    {
        _repository = repository;
    }

    [BindProperty]
    public MaszynaDto Maszyna { get; set; } = null!;

    public SelectList Sekcje { get; set; } = null!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        var maszyna = await _repository.GetByIdAsync(id.Value);

        if (maszyna is null)
        {
            return NotFound();
        }

        Maszyna = new MaszynaDto
        {
            Id = maszyna.Id,
            Nazwa = maszyna.Nazwa,
            Status = maszyna.Status,
            SekcjaId = maszyna.SekcjaId
        };

        await LoadSekcjeAsync();
        return Page();
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
            Id = Maszyna.Id,
            Nazwa = Maszyna.Nazwa,
            Status = Maszyna.Status,
            SekcjaId = Maszyna.SekcjaId
        };

        try
        {
            await _repository.UpdateAsync(entity);
            await _repository.SaveAsync();
        }
        catch (Exception)
        {
            var existing = await _repository.GetByIdAsync(Maszyna.Id);
            if (existing is null)
            {
                return NotFound();
            }

            throw;
        }

        return RedirectToPage("./Index");
    }

    private async Task LoadSekcjeAsync()
    {
        var sekcje = await _repository.GetAllSekcjeAsync();
        Sekcje = new SelectList(sekcje, "Id", "Nazwa");
    }
}
