<<<<<<< HEAD
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication1.Models;
using WebApplication1.Repositories;
using WebApplication1.Security;
=======
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication1.Models;
using WebApplication1.Repositories;
>>>>>>> origin/master

namespace WebApplication1.Pages.OrderPages;

public class IndexModel : PageModel
{
    private readonly IOrderRepository _repository;

    public IndexModel(IOrderRepository repository)
    {
        _repository = repository;
    }

    public IList<Order> Order { get; set; } = default!;

<<<<<<< HEAD
    public bool CanManageStore => User.HasClaim(AppClaimTypes.Permission, AppPermissions.ManageStore);

    public async Task OnGetAsync()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        Order = CanManageStore
            ? await _repository.GetAllAsync()
            : userId is null
                ? new List<Order>()
                : await _repository.GetAllByUserIdAsync(userId);
=======
    public async Task OnGetAsync()
    {
        Order = await _repository.GetAllAsync();
>>>>>>> origin/master
    }
}
