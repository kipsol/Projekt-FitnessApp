using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication1.Models;
using WebApplication1.Repositories;

namespace WebApplication1.Pages.OrderPages;

public class DeleteModel : PageModel
{
    private readonly IOrderRepository _repository;

    public DeleteModel(IOrderRepository repository)
    {
        _repository = repository;
    }

    [BindProperty]
    public Order Order { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id is null) return NotFound();

        var entity = await _repository.GetByIdAsync(id.Value);
        if (entity is null) return NotFound();

        Order = entity;
        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int? id)
    {
        if (id is null) return NotFound();

        await _repository.DeleteAsync(id.Value);
        await _repository.SaveAsync();
        return RedirectToPage("./Index");
    }
}
