using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication1.Models;
using WebApplication1.Repositories;

namespace WebApplication1.Pages.MealPages;

public class IndexModel : PageModel
{
    private readonly IMealRepository _repository;

    public IndexModel(IMealRepository repository)
    {
        _repository = repository;
    }

    public IList<Meal> Meal { get; set; } = default!;

    public async Task OnGetAsync()
    {
        Meal = await _repository.GetAllAsync();
    }
}
