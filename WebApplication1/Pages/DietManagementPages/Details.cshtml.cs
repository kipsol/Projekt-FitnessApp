using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication1.Models;
using WebApplication1.Repositories;

namespace WebApplication1.Pages.DietManagementPages;

public class DetailsModel : PageModel
{
    private readonly IDietRepository _repository;

    public DetailsModel(IDietRepository repository)
    {
        _repository = repository;
    }

    public Diet Diet { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id is null) return NotFound();

        var entity = await _repository.GetByIdAsync(id.Value);
        if (entity is null) return NotFound();

        Diet = entity;
        return Page();
    }
}
