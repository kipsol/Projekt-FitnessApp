using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;
using WebApplication1.Data;

namespace WebApplication1.Pages.MealPages;

public class DeleteModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public DeleteModel(ApplicationDbContext context)
    {
        _context = context;
    }

    [BindProperty]
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

    public async Task<IActionResult> OnPostAsync(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        var meal = await _context.Meals.FindAsync(id);
        if (meal != null)
        {
            Meal = meal;
            _context.Meals.Remove(Meal);
            await _context.SaveChangesAsync();
        }

        return RedirectToPage("./Index");
    }
}
