using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication1.Models;
using WebApplication1.Repositories;
<<<<<<< HEAD
using WebApplication1.Security;
=======
>>>>>>> origin/master

namespace WebApplication1.Pages.ClassEventPages;

public class IndexModel : PageModel
{
    private readonly IClassEventRepository _repository;

    public IndexModel(IClassEventRepository repository)
    {
        _repository = repository;
    }

    public IList<ClassEvent> ClassEvent { get; set; } = default!;

<<<<<<< HEAD
    public bool CanManageClasses => User.HasClaim(AppClaimTypes.Permission, AppPermissions.ManageFitnessClasses);

=======
>>>>>>> origin/master
    public async Task OnGetAsync()
    {
        ClassEvent = await _repository.GetAllAsync();
    }
}
