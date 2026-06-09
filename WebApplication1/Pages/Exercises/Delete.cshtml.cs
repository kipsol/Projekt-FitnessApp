using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication1.Models;
using WebApplication1.Repositories;

namespace WebApplication1.Pages.Exercises;

public class DeleteModel : PageModel
{
    private readonly ICwiczenieRepository _repository;

    public DeleteModel(ICwiczenieRepository repository)
    {
        _repository = repository;
    }

    [BindProperty]
    public Cwiczenie Cwiczenie { get; set; } = null!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        var cwiczenie = await _repository.GetByIdWithDetailsAsync(id.Value);

        if (cwiczenie is null)
        {
            return NotFound();
        }

        Cwiczenie = cwiczenie;
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
