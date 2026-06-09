using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApplication1.Models;
using WebApplication1.Repositories;

namespace WebApplication1.Pages.OrderPages;

public class CreateModel : PageModel
{
    private readonly IOrderRepository _orderRepository;
    private readonly IOrderItemRepository _orderItemRepository;
    private readonly IProductRepository _productRepository;

    public CreateModel(
        IOrderRepository orderRepository,
        IOrderItemRepository orderItemRepository,
        IProductRepository productRepository)
    {
        _orderRepository = orderRepository;
        _orderItemRepository = orderItemRepository;
        _productRepository = productRepository;
    }

    [BindProperty]
    public int WybranyProduktId { get; set; }

    [BindProperty]
    public int Ilosc { get; set; } = 1;

    public async Task<IActionResult> OnGetAsync(int? productId)
    {
        await LoadProductsAsync(productId);

        if (productId.HasValue)
        {
            WybranyProduktId = productId.Value;
        }

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId is null)
        {
            return Challenge();
        }

        var produkt = await _productRepository.GetByIdAsync(WybranyProduktId);
        if (produkt == null || produkt.Stock < Ilosc)
        {
            ModelState.AddModelError("", $"Brak towaru. Dostepna ilosc: {produkt?.Stock ?? 0} szt.");
            await LoadProductsAsync(WybranyProduktId);
            return Page();
        }

        var noweZamowienie = new Order
        {
            OrderDate = DateTime.Now,
            Status = "Oczekujace",
            TotalPrice = produkt.Price * Ilosc,
            UserId = userId
        };

        await _orderRepository.AddAsync(noweZamowienie);
        await _orderRepository.SaveAsync();

        var nowaPozycja = new OrderItem
        {
            OrderId = noweZamowienie.Id,
            ProductId = produkt.Id,
            Quantity = Ilosc,
            PriceAtPurchase = produkt.Price
        };

        await _orderItemRepository.AddAsync(nowaPozycja);

        produkt.Stock -= Ilosc;
        await _productRepository.UpdateAsync(produkt);

        await _orderItemRepository.SaveAsync();

        return RedirectToPage("./Index");
    }

    private async Task LoadProductsAsync(int? selectedProductId = null)
    {
        var dostepne = await _productRepository.GetAvailableAsync();
        ViewData["ProductId"] = new SelectList(dostepne, "Id", "Name", selectedProductId);
    }
}
