using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Pages.GymMachines;

public class EditModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public EditModel(ApplicationDbContext context)
    {
        _context = context;
    }

    [BindProperty]
    public Maszyna Maszyna { get; set; } = null!;

    public SelectList Sekcje { get; set; } = null!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        var maszyna = await _context.Maszyny.FindAsync(id);

        if (maszyna is null)
        {
            return NotFound();
        }

        Maszyna = maszyna;
        await LoadSekcjeAsync();
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        ModelState.Remove("Maszyna.Sekcja");
        ModelState.Remove("Maszyna.Cwiczenia");

        if (!ModelState.IsValid)
        {
            await LoadSekcjeAsync();
            return Page();
        }

        _context.Attach(Maszyna).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!await _context.Maszyny.AnyAsync(item => item.Id == Maszyna.Id))
            {
                return NotFound();
            }

            throw;
        }

        return RedirectToPage("./Index");
    }

    private async Task LoadSekcjeAsync()
    {
        Sekcje = new SelectList(await _context.Sekcje.OrderBy(item => item.Nazwa).ToListAsync(), "Id", "Nazwa");
    }
}
