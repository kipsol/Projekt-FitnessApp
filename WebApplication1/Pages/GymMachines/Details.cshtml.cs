using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Pages.GymMachines;

public class DetailsModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public DetailsModel(ApplicationDbContext context)
    {
        _context = context;
    }

    public Maszyna Maszyna { get; set; } = null!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        var maszyna = await _context.Maszyny
            .Include(item => item.Sekcja)
            .Include(item => item.Cwiczenia)
            .FirstOrDefaultAsync(item => item.Id == id);

        if (maszyna is null)
        {
            return NotFound();
        }

        Maszyna = maszyna;
        return Page();
    }
}
