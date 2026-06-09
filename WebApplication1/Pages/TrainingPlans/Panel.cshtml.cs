using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication1.Models;
using WebApplication1.Repositories;

namespace WebApplication1.Pages;

public class PanelModel : PageModel
{
    private readonly IPlanTreningowyRepository _repository;

    public PanelModel(IPlanTreningowyRepository repository)
    {
        _repository = repository;
    }

    public IList<PlanTreningowy> PlanyTreningowe { get; set; } = new List<PlanTreningowy>();

    public async Task OnGetAsync()
    {
        PlanyTreningowe = await _repository.GetAllAsync();
    }
}
