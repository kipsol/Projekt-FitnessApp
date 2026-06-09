using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Pages.OrderItemPages;

public class CreateModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public CreateModel(ApplicationDbContext context)
    {
        _context = context;
    }

    [BindProperty]
    public OrderItem OrderItem { get; set; } = default!;

    public IActionResult OnGet()
    {
        ViewData["OrderId"] = new SelectList(_context.Orders.OrderByDescending(o => o.Id), "Id", "Id");

        ViewData["ProductId"] = new SelectList(_context.Products.Where(p => p.Stock > 0), "Id", "Name");

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        ModelState.Remove("OrderItem.Order");
        ModelState.Remove("OrderItem.Product");
        ModelState.Remove("OrderItem.PriceAtPurchase");

        if (!ModelState.IsValid)
        {
            ViewData["OrderId"] = new SelectList(_context.Orders.OrderByDescending(o => o.Id), "Id", "Id");
            ViewData["ProductId"] = new SelectList(_context.Products.Where(p => p.Stock > 0), "Id", "Name");
            return Page();
        }

        var produkt = await _context.Products.FindAsync(OrderItem.ProductId);
        if (produkt == null)
        {
            ModelState.AddModelError("", "Wybrany produkt nie istnieje.");
            return Page();
        }

        if (produkt.Stock < OrderItem.Quantity)
        {
            ModelState.AddModelError("", $"Brak wystarczającej ilości towaru w magazynie. Dostępne: {produkt.Stock} szt.");
            OnGet();
            return Page();
        }

        OrderItem.PriceAtPurchase = produkt.Price;

        produkt.Stock -= OrderItem.Quantity;

        var zamowienie = await _context.Orders.FindAsync(OrderItem.OrderId);
        if (zamowienie != null)
        {
            zamowienie.TotalPrice += OrderItem.PriceAtPurchase * OrderItem.Quantity;
        }

        _context.OrderItems.Add(OrderItem);
        await _context.SaveChangesAsync();

        return RedirectToPage("./Index");
    }
}
