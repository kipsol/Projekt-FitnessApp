using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Pages.Sections;

public class DetailsModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public DetailsModel(ApplicationDbContext context)
    {
        _context = context;
    }

    public Sekcja Sekcja { get; set; } = null!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        var sekcja = await _context.Sekcje
            .Include(item => item.Maszyny)
            .FirstOrDefaultAsync(item => item.Id == id);

        if (sekcja is null)
        {
            return NotFound();
        }

        Sekcja = sekcja;
        return Page();
    }
}
