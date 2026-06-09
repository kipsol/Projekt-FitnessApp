using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication1.Models;
using WebApplication1.Repositories;
using WebApplication1.Security;

namespace WebApplication1.Pages.GymMachines;

public class IndexModel : PageModel
{
    private readonly IMaszynaRepository _repository;

    public IndexModel(IMaszynaRepository repository)
    {
        _repository = repository;
    }

    public IList<Maszyna> Maszyny { get; set; } = new List<Maszyna>();

    public bool CanManageGym => User.HasClaim(AppClaimTypes.Permission, AppPermissions.ManageGymStructure);

    public async Task OnGetAsync()
    {
        Maszyny = await _repository.GetAllAsync();
    }
}
