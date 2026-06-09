using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Pages.GymMachines;

public class DeleteModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public DeleteModel(ApplicationDbContext context)
    {
        _context = context;
    }

    [BindProperty]
    public Maszyna Maszyna { get; set; } = null!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        var maszyna = await _context.Maszyny
            .Include(item => item.Sekcja)
            .FirstOrDefaultAsync(item => item.Id == id);

        if (maszyna is null)
        {
            return NotFound();
        }

        Maszyna = maszyna;
        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        var maszyna = await _context.Maszyny.FindAsync(id);

        if (maszyna is not null)
        {
            _context.Maszyny.Remove(maszyna);
            await _context.SaveChangesAsync();
        }

        return RedirectToPage("./Index");
    }
}
