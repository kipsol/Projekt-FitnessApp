using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication1.Models;
using WebApplication1.Repositories;
using WebApplication1.Security;

namespace WebApplication1.Pages.Sections;

public class IndexModel : PageModel
{
    private readonly ISekcjaRepository _repository;

    public IndexModel(ISekcjaRepository repository)
    {
        _repository = repository;
    }

    public IList<Sekcja> Sekcje { get; set; } = new List<Sekcja>();

    public bool CanManageGym => User.HasClaim(AppClaimTypes.Permission, AppPermissions.ManageGymStructure);

    public async Task OnGetAsync()
    {
        Sekcje = await _repository.GetAllAsync();
    }
}
