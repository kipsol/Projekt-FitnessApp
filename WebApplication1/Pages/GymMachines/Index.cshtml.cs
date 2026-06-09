using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Pages.GymMachines;

public class IndexModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public IndexModel(ApplicationDbContext context)
    {
        _context = context;
    }

    public IList<Maszyna> Maszyny { get; set; } = new List<Maszyna>();

    public async Task OnGetAsync()
    {
        Maszyny = await _context.Maszyny
            .Include(maszyna => maszyna.Sekcja)
            .Include(maszyna => maszyna.Cwiczenia)
            .OrderBy(maszyna => maszyna.Nazwa)
            .ToListAsync();
    }
}
