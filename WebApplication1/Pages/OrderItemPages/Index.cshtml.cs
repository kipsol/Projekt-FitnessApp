using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Pages.OrderItemPages;

public class IndexModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public IndexModel(ApplicationDbContext context)
    {
        _context = context;
    }

    public IList<OrderItem> OrderItem { get; set; } = default!;

    public async Task OnGetAsync()
    {
        OrderItem = await _context.OrderItems
            .Include(o => o.Order)
            .Include(o => o.Product)
            .ToListAsync();
    }

    public async Task<IActionResult> OnPostUpdateStatusAsync(int orderId, string nowyStatus)
    {
        var zamowienie = await _context.Orders.FindAsync(orderId);
        if (zamowienie != null)
        {
            zamowienie.Status = nowyStatus;
            await _context.SaveChangesAsync();
        }

        return RedirectToPage();
    }
}
