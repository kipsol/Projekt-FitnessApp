using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;
using WebApplication1.Data;

namespace WebApplication1.Pages.DietManagementPages;

public class DeleteModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public DeleteModel(ApplicationDbContext context)
    {
        _context = context;
    }

    [BindProperty]
    public Diet Diet { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        var diet = await _context.Diets.FirstOrDefaultAsync(m => m.Id == id);
        if (diet is null)
        {
            return NotFound();
        }
        else
        {
            Diet = diet;
        }

        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        var diet = await _context.Diets.FindAsync(id);
        if (diet != null)
        {
            Diet = diet;
            _context.Diets.Remove(Diet);
            await _context.SaveChangesAsync();
        }

        return RedirectToPage("/DietPages/Index");
    }
}
