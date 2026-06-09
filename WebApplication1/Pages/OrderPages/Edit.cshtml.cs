using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication1.DTOs;
using WebApplication1.Models;
using WebApplication1.Repositories;

namespace WebApplication1.Pages.OrderPages;

public class EditModel : PageModel
{
    private readonly IOrderRepository _repository;

    public EditModel(IOrderRepository repository)
    {
        _repository = repository;
    }

    [BindProperty]
    public OrderDto Input { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id is null) return NotFound();

        var entity = await _repository.GetByIdAsync(id.Value);
        if (entity is null) return NotFound();

        Input = new OrderDto
        {
            Id = entity.Id,
            OrderDate = entity.OrderDate,
            TotalPrice = entity.TotalPrice,
            Status = entity.Status
        };

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid) return Page();

        var entity = new Order
        {
            Id = Input.Id,
            OrderDate = Input.OrderDate,
            TotalPrice = Input.TotalPrice,
            Status = Input.Status
        };

        await _repository.UpdateAsync(entity);
        await _repository.SaveAsync();
        return RedirectToPage("./Index");
    }
}
