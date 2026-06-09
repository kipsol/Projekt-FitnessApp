using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication1.Models;
using WebApplication1.Repositories;

namespace WebApplication1.Pages.OrderPages;

public class IndexModel : PageModel
{
    private readonly IOrderRepository _repository;

    public IndexModel(IOrderRepository repository)
    {
        _repository = repository;
    }

    public IList<Order> Order { get; set; } = default!;

    public async Task OnGetAsync()
    {
        Order = await _repository.GetAllAsync();
    }
}
