using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;
using WebApplication1.Data;

namespace WebApplication1.Pages.DietPlanDayPages;

public class DetailsModel : PageModel
{
    private readonly ApplicationDbContext _context;
    public DetailsModel(ApplicationDbContext context)
    {
        _context = context;
    }

    public DietPlanDay DietPlanDay { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        var dietplanday = await _context.DietPlanDays.FirstOrDefaultAsync(m => m.Id == id);
        if (dietplanday is null)
        {
            return NotFound();
        }
        else
        {
            DietPlanDay = dietplanday;
        }

        return Page();
    }
}
