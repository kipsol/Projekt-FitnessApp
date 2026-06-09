using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Pages.Sections;

public class DeleteModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public DeleteModel(ApplicationDbContext context)
    {
        _context = context;
    }

    [BindProperty]
    public Sekcja Sekcja { get; set; } = null!;

    public bool HasMaszyny { get; set; }

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
        HasMaszyny = sekcja.Maszyny.Any();
        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int? id)
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
            return RedirectToPage("./Index");
        }

        if (sekcja.Maszyny.Any())
        {
            Sekcja = sekcja;
            HasMaszyny = true;
            ModelState.AddModelError(string.Empty, "Nie mozna usunac sekcji, ktora ma przypisane maszyny.");
            return Page();
        }

        _context.Sekcje.Remove(sekcja);
        await _context.SaveChangesAsync();

        return RedirectToPage("./Index");
    }
}
