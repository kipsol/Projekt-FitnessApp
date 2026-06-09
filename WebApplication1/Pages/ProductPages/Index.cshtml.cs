using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication1.Models;
using WebApplication1.Repositories;

namespace WebApplication1.Pages.ProductPages;

public class IndexModel : PageModel
{
    private readonly IProductRepository _repository;

    public IndexModel(IProductRepository repository)
    {
        _repository = repository;
    }

    public IList<Product> Product { get; set; } = default!;

    public async Task OnGetAsync()
    {
        Product = await _repository.GetAllAsync();
    }
}
