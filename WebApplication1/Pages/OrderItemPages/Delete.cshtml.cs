using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;
using WebApplication1.Data;

namespace WebApplication1.Pages.OrderItemPages;

public class DeleteModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public DeleteModel(ApplicationDbContext context)
    {
        _context = context;
    }

    [BindProperty]
    public OrderItem OrderItem { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        var orderitem = await _context.OrderItems.FirstOrDefaultAsync(m => m.Id == id);
        if (orderitem is null)
        {
            return NotFound();
        }
        else
        {
            OrderItem = orderitem;
        }

        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        var orderitem = await _context.OrderItems.FindAsync(id);
        if (orderitem != null)
        {
            OrderItem = orderitem;
            _context.OrderItems.Remove(OrderItem);
            await _context.SaveChangesAsync();
        }

        return RedirectToPage("./Index");
    }
}
