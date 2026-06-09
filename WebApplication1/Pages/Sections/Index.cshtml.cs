using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Pages.Sections;

public class IndexModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public IndexModel(ApplicationDbContext context)
    {
        _context = context;
    }

    public IList<Sekcja> Sekcje { get; set; } = new List<Sekcja>();

    public async Task OnGetAsync()
    {
        Sekcje = await _context.Sekcje
            .Include(sekcja => sekcja.Maszyny)
            .OrderBy(sekcja => sekcja.Nazwa)
            .ToListAsync();
    }
}
