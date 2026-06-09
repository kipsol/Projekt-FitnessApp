using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication1.Models;
using WebApplication1.Repositories;

namespace WebApplication1.Pages.Exercises;

public class IndexModel : PageModel
{
    private readonly ICwiczenieRepository _repository;

    public IndexModel(ICwiczenieRepository repository)
    {
        _repository = repository;
    }

    public IList<Cwiczenie> Cwiczenia { get; set; } = new List<Cwiczenie>();

    public async Task OnGetAsync()
    {
        Cwiczenia = await _repository.GetAllAsync();
    }
}
