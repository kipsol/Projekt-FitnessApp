using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication1.Models;
using WebApplication1.Repositories;

namespace WebApplication1.Pages.TrainingPlans;

public class IndexModel : PageModel
{
    private readonly IPlanTreningowyRepository _repository;

    public IndexModel(IPlanTreningowyRepository repository)
    {
        _repository = repository;
    }

    public IList<PlanTreningowy> PlanyTreningowe { get; set; } = new List<PlanTreningowy>();

    public async Task OnGetAsync()
    {
        PlanyTreningowe = await _repository.GetAllAsync();
    }
}
