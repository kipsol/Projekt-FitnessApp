using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Pages.OrderPages;

public class CreateModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public CreateModel(ApplicationDbContext context)
    {
        _context = context;
    }

    [BindProperty]
    public int WybranyProduktId { get; set; }

    [BindProperty]
    public int Ilosc { get; set; } = 1;

    public IActionResult OnGet()
    {
        ViewData["ProductId"] = new SelectList(_context.Products.Where(p => p.Stock > 0), "Id", "Name");
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        var produkt = await _context.Products.FindAsync(WybranyProduktId);
        if (produkt == null || produkt.Stock < Ilosc)
        {
            ModelState.AddModelError("", $"Brak towaru. Dostępna ilość: {produkt?.Stock ?? 0} szt.");
            ViewData["ProductId"] = new SelectList(_context.Products.Where(p => p.Stock > 0), "Id", "Name");
            return Page();
        }

        var noweZamowienie = new Order
        {
            OrderDate = DateTime.Now,
            Status = "Oczekujące",
            TotalPrice = produkt.Price * Ilosc
        };

        _context.Orders.Add(noweZamowienie);
        await _context.SaveChangesAsync();

        var nowaPozycja = new OrderItem
        {
            OrderId = noweZamowienie.Id,
            ProductId = produkt.Id,
            Quantity = Ilosc,
            PriceAtPurchase = produkt.Price
        };

        _context.OrderItems.Add(nowaPozycja);

        produkt.Stock -= Ilosc;

        await _context.SaveChangesAsync();

        return RedirectToPage("./Index");
    }
}
