using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Pages.TrainingPlans;

public class DeleteModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public DeleteModel(ApplicationDbContext context)
    {
        _context = context;
    }

    [BindProperty]
    public PlanTreningowy PlanTreningowy { get; set; } = null!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        var plan = await _context.PlanyTreningowe
            .Include(item => item.PozycjePlanu)
            .FirstOrDefaultAsync(item => item.Id == id);

        if (plan is null)
        {
            return NotFound();
        }

        PlanTreningowy = plan;
        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        var plan = await _context.PlanyTreningowe
            .Include(item => item.PozycjePlanu)
            .FirstOrDefaultAsync(item => item.Id == id);

        if (plan is not null)
        {
            _context.PlanyTreningowe.Remove(plan);
            await _context.SaveChangesAsync();
        }

        return RedirectToPage("./Index");
    }
}
