using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;
using WebApplication1.Data;

namespace WebApplication1.Pages.ClassEventPages;

public class DetailsModel : PageModel
{
    private readonly ApplicationDbContext _context;
    public DetailsModel(ApplicationDbContext context)
    {
        _context = context;
    }

    public ClassEvent ClassEvent { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        var classevent = await _context.ClassEvents.FirstOrDefaultAsync(m => m.Id == id);
        if (classevent is null)
        {
            return NotFound();
        }
        else
        {
            ClassEvent = classevent;
        }

        return Page();
    }
}
