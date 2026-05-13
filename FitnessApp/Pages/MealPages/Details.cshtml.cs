using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using FitnessApp.Models;
using FitnessApp.Data;

namespace FitnessApp.Pages.MealPages;

public class DetailsModel : PageModel
{
    private readonly ApplicationDbContext _context;
    public DetailsModel(ApplicationDbContext context)
    {
        _context = context;
    }

    public Meal Meal { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        var meal = await _context.Meals.FirstOrDefaultAsync(m => m.Id == id);
        if (meal is null)
        {
            return NotFound();
        }
        else
        {
            Meal = meal;
        }

        return Page();
    }
}
