using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication1.Models;
using WebApplication1.Repositories;

namespace WebApplication1.Pages.MuscleGroups;

public class IndexModel : PageModel
{
    private readonly IPartiaMiesniowaRepository _repository;

    public IndexModel(IPartiaMiesniowaRepository repository)
    {
        _repository = repository;
    }

    public IList<PartiaMiesniowa> PartieMiesniowe { get; set; } = new List<PartiaMiesniowa>();

    public async Task OnGetAsync()
    {
        PartieMiesniowe = await _repository.GetAllAsync();
    }
}
