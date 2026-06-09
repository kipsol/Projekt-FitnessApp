using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication1.Models;
using WebApplication1.Repositories;
<<<<<<< HEAD
using WebApplication1.Security;
=======
>>>>>>> origin/master

namespace WebApplication1.Pages.Sections;

public class IndexModel : PageModel
{
    private readonly ISekcjaRepository _repository;

    public IndexModel(ISekcjaRepository repository)
    {
        _repository = repository;
    }

    public IList<Sekcja> Sekcje { get; set; } = new List<Sekcja>();

<<<<<<< HEAD
    public bool CanManageGym => User.HasClaim(AppClaimTypes.Permission, AppPermissions.ManageGymStructure);

=======
>>>>>>> origin/master
    public async Task OnGetAsync()
    {
        Sekcje = await _repository.GetAllAsync();
    }
}
