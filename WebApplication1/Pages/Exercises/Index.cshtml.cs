using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Pages.Exercises;

public class IndexModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public IndexModel(ApplicationDbContext context)
    {
        _context = context;
    }

    public IList<Cwiczenie> Cwiczenia { get; set; } = new List<Cwiczenie>();

    public async Task OnGetAsync()
    {
        Cwiczenia = await _context.Cwiczenia
            .Include(cwiczenie => cwiczenie.PartiaMiesniowa)
            .Include(cwiczenie => cwiczenie.Maszyna)
            .Include(cwiczenie => cwiczenie.PozycjePlanu)
            .ThenInclude(pozycja => pozycja.PlanTreningowy)
            .OrderBy(cwiczenie => cwiczenie.Nazwa)
            .ToListAsync();
    }
}
