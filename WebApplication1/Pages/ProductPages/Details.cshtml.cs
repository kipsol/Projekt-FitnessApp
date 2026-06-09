using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication1.Models;
using WebApplication1.Repositories;

namespace WebApplication1.Pages.ProductPages;

public class DetailsModel : PageModel
{
    private readonly IProductRepository _repository;

    public DetailsModel(IProductRepository repository)
    {
        _repository = repository;
    }

    public Product Product { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id is null) return NotFound();

        var entity = await _repository.GetByIdAsync(id.Value);
        if (entity is null) return NotFound();

        Product = entity;
        return Page();
    }
}
