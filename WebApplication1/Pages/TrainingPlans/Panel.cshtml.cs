using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Pages;

public class PanelModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public PanelModel(ApplicationDbContext context)
    {
        _context = context;
    }

    public IList<PlanTreningowy> PlanyTreningowe { get; set; } = new List<PlanTreningowy>();

    public async Task OnGetAsync()
    {
        PlanyTreningowe = await _context.PlanyTreningowe
            .Include(plan => plan.Cwiczenia)
            .OrderBy(plan => plan.Nazwa)
            .ToListAsync();
    }
}
