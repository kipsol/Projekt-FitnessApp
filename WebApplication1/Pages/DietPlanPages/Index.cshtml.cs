using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;
using WebApplication1.Data;

namespace WebApplication1.Pages.DietPlanDayPages;

public class IndexModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public IndexModel(ApplicationDbContext context)
    {
        _context = context;
    }

    public IList<DietPlanDay> DietPlanDay { get; set; } = default!;

    public async Task OnGetAsync()
    {
        DietPlanDay = await _context.DietPlanDays.ToListAsync();
    }
}
