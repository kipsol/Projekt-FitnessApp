using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;
using WebApplication1.Data;

namespace WebApplication1.Pages.MealPages;

public class IndexModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public IndexModel(ApplicationDbContext context)
    {
        _context = context;
    }

    public IList<Meal> Meal { get; set; } = default!;

    public async Task OnGetAsync()
    {
        Meal = await _context.Meals
            .Include(m => m.PlanDays)
            .ToListAsync();
    }
}
