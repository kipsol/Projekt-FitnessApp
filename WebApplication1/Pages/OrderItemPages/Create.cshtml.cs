using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApplication1.DTOs;
using WebApplication1.Models;
using WebApplication1.Repositories;

namespace WebApplication1.Pages.OrderItemPages;

public class CreateModel : PageModel
{
    private readonly IOrderItemRepository _repository;

    public CreateModel(IOrderItemRepository repository)
    {
        _repository = repository;
    }

    [BindProperty]
    public OrderItemDto Input { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync()
    {
        var zamowienia = await _repository.GetAllOrdersAsync();
        var produkty = await _repository.GetAvailableProductsAsync();
        ViewData["OrderId"] = new SelectList(zamowienia, "Id", "Id");
        ViewData["ProductId"] = new SelectList(produkty, "Id", "Name");
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            var zamowienia = await _repository.GetAllOrdersAsync();
            var produkty = await _repository.GetAvailableProductsAsync();
            ViewData["OrderId"] = new SelectList(zamowienia, "Id", "Id");
            ViewData["ProductId"] = new SelectList(produkty, "Id", "Name");
            return Page();
        }

        var produkt = await _repository.GetProductByIdAsync(Input.ProductId);
        if (produkt == null)
        {
            ModelState.AddModelError("", "Wybrany produkt nie istnieje.");
            return Page();
        }

        if (produkt.Stock < Input.Quantity)
        {
            ModelState.AddModelError("", $"Brak wystarczającej ilości towaru. Dostępne: {produkt.Stock} szt.");
            return Page();
        }

        var entity = new OrderItem
        {
            OrderId = Input.OrderId,
            ProductId = Input.ProductId,
            Quantity = Input.Quantity,
            PriceAtPurchase = produkt.Price
        };

        produkt.Stock -= Input.Quantity;

        var zamowienie = await _repository.GetOrderByIdAsync(Input.OrderId);
        if (zamowienie is not null)
        {
            zamowienie.TotalPrice += entity.PriceAtPurchase * entity.Quantity;
        }

        await _repository.AddAsync(entity);
        await _repository.SaveAsync();
        return RedirectToPage("./Index");
    }
}
