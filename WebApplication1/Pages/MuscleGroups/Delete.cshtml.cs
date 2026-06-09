using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Pages.MuscleGroups;

public class DeleteModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public DeleteModel(ApplicationDbContext context)
    {
        _context = context;
    }

    [BindProperty]
    public PartiaMiesniowa PartiaMiesniowa { get; set; } = null!;

    public bool HasCwiczenia { get; set; }

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
        HasCwiczenia = partia.Cwiczenia.Any();
        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int? id)
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
            return RedirectToPage("./Index");
        }

        if (partia.Cwiczenia.Any())
        {
            PartiaMiesniowa = partia;
            HasCwiczenia = true;
            ModelState.AddModelError(string.Empty, "Nie mozna usunac partii miesniowej, ktora ma przypisane cwiczenia.");
            return Page();
        }

        _context.PartieMiesniowe.Remove(partia);
        await _context.SaveChangesAsync();

        return RedirectToPage("./Index");
    }
}
