using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication1.Models;
using WebApplication1.Repositories;

namespace WebApplication1.Pages.OrderItemPages;

public class DetailsModel : PageModel
{
    private readonly IOrderItemRepository _repository;

    public DetailsModel(IOrderItemRepository repository)
    {
        _repository = repository;
    }

    public OrderItem OrderItem { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id is null) return NotFound();

        var entity = await _repository.GetByIdAsync(id.Value);
        if (entity is null) return NotFound();

        OrderItem = entity;
        return Page();
    }
}
