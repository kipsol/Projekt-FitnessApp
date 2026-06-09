using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication1.Models;
using WebApplication1.Repositories;
<<<<<<< HEAD
using WebApplication1.Security;
=======
>>>>>>> origin/master

namespace WebApplication1.Pages.ProductPages;

public class IndexModel : PageModel
{
    private readonly IProductRepository _repository;

    public IndexModel(IProductRepository repository)
    {
        _repository = repository;
    }

    public IList<Product> Product { get; set; } = default!;

<<<<<<< HEAD
    public bool CanManageStore => User.HasClaim(AppClaimTypes.Permission, AppPermissions.ManageStore);

=======
>>>>>>> origin/master
    public async Task OnGetAsync()
    {
        Product = await _repository.GetAllAsync();
    }
}
