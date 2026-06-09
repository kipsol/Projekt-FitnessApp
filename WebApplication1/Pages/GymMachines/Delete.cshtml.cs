using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication1.Models;
using WebApplication1.Repositories;

namespace WebApplication1.Pages.GymMachines;

public class DeleteModel : PageModel
{
    private readonly IMaszynaRepository _repository;

    public DeleteModel(IMaszynaRepository repository)
    {
        _repository = repository;
    }

    [BindProperty]
    public Maszyna Maszyna { get; set; } = null!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        var maszyna = await _repository.GetByIdWithDetailsAsync(id.Value);

        if (maszyna is null)
        {
            return NotFound();
        }

        Maszyna = maszyna;
        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        await _repository.DeleteAsync(id.Value);
        await _repository.SaveAsync();

        return RedirectToPage("./Index");
    }
}
