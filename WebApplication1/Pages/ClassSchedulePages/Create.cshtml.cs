using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Pages.ClassSchedulePages;

public class CreateModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public CreateModel(ApplicationDbContext context)
    {
        _context = context;
    }

    public List<ClassEvent> DostepneZajecia { get; set; } = new();

    public List<int> JuzZapisanyId { get; set; } = new();

    public async Task OnGetAsync()
    {
        DostepneZajecia = await _context.ClassEvents.OrderBy(c => c.Day).ThenBy(c => c.StartTime).ToListAsync();

        JuzZapisanyId = await _context.ClassSchedules
            .Select(cs => cs.ClassEventId)
            .ToListAsync();
    }

    public async Task<IActionResult> OnPostZapiszAsync(int classEventId)
    {
        var nowyZapis = new ClassSchedule
        {
            ClassEventId = classEventId
        };

        _context.ClassSchedules.Add(nowyZapis);
        await _context.SaveChangesAsync();

        return RedirectToPage("./Index");
    }
}
