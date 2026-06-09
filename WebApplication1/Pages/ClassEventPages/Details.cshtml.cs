using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication1.Models;
using WebApplication1.Repositories;

namespace WebApplication1.Pages.ClassEventPages;

public class DetailsModel : PageModel
{
    private readonly IClassEventRepository _repository;

    public DetailsModel(IClassEventRepository repository)
    {
        _repository = repository;
    }

    public ClassEvent ClassEvent { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        var entity = await _repository.GetByIdAsync(id.Value);
        if (entity is null)
        {
            return NotFound();
        }

        ClassEvent = entity;
        return Page();
    }
}
