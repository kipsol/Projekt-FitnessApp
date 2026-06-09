using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApplication1.Pages;

public class IndexModel : PageModel
{
<<<<<<< HEAD
    public IActionResult OnGet()
        => RedirectToPage("/TrainingPlans/Panel");
=======
    private readonly ApplicationDbContext _context;

    public IndexModel(ApplicationDbContext context)
    {
        _context = context;
    }

    public IList<PlanTreningowy> PlanyTreningowe { get; set; } = new List<PlanTreningowy>();

    public async Task OnGetAsync()
    {
        PlanyTreningowe = await _context.PlanyTreningowe
            .Include(plan => plan.PozycjePlanu)
            .OrderBy(plan => plan.Nazwa)
            .ToListAsync();
    }
>>>>>>> origin/master
}
