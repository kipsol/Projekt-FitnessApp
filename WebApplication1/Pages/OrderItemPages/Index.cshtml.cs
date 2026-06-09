using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication1.Models;
using WebApplication1.Repositories;

namespace WebApplication1.Pages.OrderItemPages;

public class IndexModel : PageModel
{
    private readonly IOrderItemRepository _orderItemRepository;
    private readonly IOrderRepository _orderRepository;

    public IndexModel(IOrderItemRepository orderItemRepository, IOrderRepository orderRepository)
    {
        _orderItemRepository = orderItemRepository;
        _orderRepository = orderRepository;
    }

    public IList<OrderItem> OrderItem { get; set; } = default!;

    public async Task OnGetAsync()
    {
        OrderItem = await _orderItemRepository.GetAllWithDetailsAsync();
    }

    public async Task<IActionResult> OnPostUpdateStatusAsync(int orderId, string nowyStatus)
    {
        var zamowienie = await _orderRepository.GetByIdAsync(orderId);
        if (zamowienie is not null)
        {
            zamowienie.Status = nowyStatus;
            await _orderRepository.UpdateAsync(zamowienie);
            await _orderRepository.SaveAsync();
        }

        return RedirectToPage();
    }
}
