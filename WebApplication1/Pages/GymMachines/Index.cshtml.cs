using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication1.Models;
using WebApplication1.Repositories;
<<<<<<< HEAD
using WebApplication1.Security;
=======
>>>>>>> origin/master

namespace WebApplication1.Pages.GymMachines;

public class IndexModel : PageModel
{
    private readonly IMaszynaRepository _repository;

    public IndexModel(IMaszynaRepository repository)
    {
        _repository = repository;
    }

    public IList<Maszyna> Maszyny { get; set; } = new List<Maszyna>();

<<<<<<< HEAD
    public bool CanManageGym => User.HasClaim(AppClaimTypes.Permission, AppPermissions.ManageGymStructure);

=======
>>>>>>> origin/master
    public async Task OnGetAsync()
    {
        Maszyny = await _repository.GetAllAsync();
    }
}
