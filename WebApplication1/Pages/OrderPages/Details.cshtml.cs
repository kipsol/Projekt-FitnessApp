using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication1.Models;
using WebApplication1.Repositories;
using WebApplication1.Security;

namespace WebApplication1.Pages.OrderPages;

public class DetailsModel : PageModel
{
    private readonly IOrderRepository _repository;

    public DetailsModel(IOrderRepository repository)
    {
        _repository = repository;
    }

    public Order Order { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id is null) return NotFound();

        var entity = await _repository.GetByIdAsync(id.Value);
        if (entity is null) return NotFound();

        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var canManageStore = User.HasClaim(AppClaimTypes.Permission, AppPermissions.ManageStore);

        if (!canManageStore && entity.UserId != userId)
        {
            return Forbid();
        }

        Order = entity;
        return Page();
    }
}
