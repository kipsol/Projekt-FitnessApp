using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using FitnessApp.Models;
using FitnessApp.Data;

namespace FitnessApp.Pages.MealPages;

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
        Meal = await _context.Meals.ToListAsync();
    }
}
