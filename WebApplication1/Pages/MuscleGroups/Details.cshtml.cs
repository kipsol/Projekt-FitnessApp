using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Pages.MuscleGroups;

public class DetailsModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public DetailsModel(ApplicationDbContext context)
    {
        _context = context;
    }

    public PartiaMiesniowa PartiaMiesniowa { get; set; } = null!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        var partia = await _context.PartieMiesniowe
            .Include(item => item.Cwiczenia)
            .FirstOrDefaultAsync(item => item.Id == id);

        if (partia is null)
        {
            return NotFound();
        }

        PartiaMiesniowa = partia;
        return Page();
    }
}
