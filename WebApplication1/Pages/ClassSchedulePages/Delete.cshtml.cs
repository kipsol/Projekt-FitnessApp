using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;
using WebApplication1.Data;

namespace WebApplication1.Pages.ClassSchedulePages;

public class DeleteModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public DeleteModel(ApplicationDbContext context)
    {
        _context = context;
    }

    [BindProperty]
    public ClassSchedule ClassSchedule { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        var classschedule = await _context.ClassSchedules.FirstOrDefaultAsync(m => m.Id == id);
        if (classschedule is null)
        {
            return NotFound();
        }
        else
        {
            ClassSchedule = classschedule;
        }

        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        var classschedule = await _context.ClassSchedules.FindAsync(id);
        if (classschedule != null)
        {
            ClassSchedule = classschedule;
            _context.ClassSchedules.Remove(ClassSchedule);
            await _context.SaveChangesAsync();
        }

        return RedirectToPage("./Index");
    }
}
