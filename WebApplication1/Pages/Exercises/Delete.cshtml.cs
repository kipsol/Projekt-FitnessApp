using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Pages.Exercises;

public class DeleteModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public DeleteModel(ApplicationDbContext context)
    {
        _context = context;
    }

    [BindProperty]
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
            .FirstOrDefaultAsync(item => item.Id == id);

        if (cwiczenie is null)
        {
            return NotFound();
        }

        Cwiczenie = cwiczenie;
        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        var cwiczenie = await _context.Cwiczenia.FindAsync(id);

        if (cwiczenie is not null)
        {
            _context.Cwiczenia.Remove(cwiczenie);
            await _context.SaveChangesAsync();
        }

        return RedirectToPage("./Index");
    }
}
