using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication1.Models;
using WebApplication1.Repositories;

namespace WebApplication1.Pages.DietPlanDayPages;

public class IndexModel : PageModel
{
    private readonly IDietPlanDayRepository _repository;

    public IndexModel(IDietPlanDayRepository repository)
    {
        _repository = repository;
    }

    public IList<DietPlanDay> DietPlanDay { get; set; } = default!;

    public async Task OnGetAsync()
    {
        DietPlanDay = await _repository.GetAllAsync();
    }
}
