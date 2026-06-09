using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Pages.Exercises;

public class DetailsModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public DetailsModel(ApplicationDbContext context)
    {
        _context = context;
    }

    public Cwiczenie Cwiczenie { get; set; } = null!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        var cwiczenie = await _context.Cwiczenia
            .Include(item => item.PartiaMiesniowa)
            .Include(item => item.Maszyna)
            .Include(item => item.PozycjePlanu)
            .ThenInclude(pozycja => pozycja.PlanTreningowy)
            .FirstOrDefaultAsync(item => item.Id == id);

        if (cwiczenie is null)
        {
            return NotFound();
        }

        Cwiczenie = cwiczenie;
        return Page();
    }
}
