using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Pages.MuscleGroups;

public class IndexModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public IndexModel(ApplicationDbContext context)
    {
        _context = context;
    }

    public IList<PartiaMiesniowa> PartieMiesniowe { get; set; } = new List<PartiaMiesniowa>();

    public async Task OnGetAsync()
    {
        PartieMiesniowe = await _context.PartieMiesniowe
            .Include(partia => partia.Cwiczenia)
            .OrderBy(partia => partia.Nazwa)
            .ToListAsync();
    }
}
