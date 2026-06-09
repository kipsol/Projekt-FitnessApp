using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;
using WebApplication1.Data;

namespace WebApplication1.Pages.ClassSchedulePages;

public class DetailsModel : PageModel
{
    private readonly ApplicationDbContext _context;
    public DetailsModel(ApplicationDbContext context)
    {
        _context = context;
    }

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
}
