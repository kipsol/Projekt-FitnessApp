using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication1.Models;
using WebApplication1.Repositories;
using WebApplication1.Security;

namespace WebApplication1.Pages.ProductPages;

public class IndexModel : PageModel
{
    private readonly IProductRepository _repository;

    public IndexModel(IProductRepository repository)
    {
        _repository = repository;
    }

    public IList<Product> Product { get; set; } = default!;

    public bool CanManageStore => User.HasClaim(AppClaimTypes.Permission, AppPermissions.ManageStore);

    public async Task OnGetAsync()
    {
        Product = await _repository.GetAllAsync();
    }
}
