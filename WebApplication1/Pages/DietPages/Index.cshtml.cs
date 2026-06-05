using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Pages.DietPages;

public class IndexModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public IndexModel(ApplicationDbContext context)
    {
        _context = context;
    }

    public Diet? Dieta { get; set; }

    public Dictionary<DayOfWeek, List<DietPlanDay>> PosiłkiNaDni { get; set; } = new();

    public List<SelectListItem> WszystkieDiety { get; set; } = new();
    public int WybraneDietId { get; set; }

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        var dietyZbazy = await _context.Diets.ToListAsync();

        if (!dietyZbazy.Any())
        {
            return Page();
        }

        WszystkieDiety = dietyZbazy.Select(d => new SelectListItem
        {
            Value = d.Id.ToString(),
            Text = d.Name,
            Selected = id.HasValue && d.Id == id.Value
        }).ToList();

        int idDoPobrania = id ?? dietyZbazy.First().Id;
        WybraneDietId = idDoPobrania;

        Dieta = await _context.Diets
            .Include(d => d.PlanDays)
            .ThenInclude(pd => pd.Meal)
            .FirstOrDefaultAsync(m => m.Id == idDoPobrania);

        foreach (DayOfWeek dzien in Enum.GetValues(typeof(DayOfWeek)))
        {
            PosiłkiNaDni[dzien] = Dieta?.PlanDays.Where(pd => pd.Day == dzien).ToList() ?? new List<DietPlanDay>();
        }

        return Page();
    }

    public async Task<IActionResult> OnPostDeleteMealAsync(int planDayId, int currentDietId)
    {
        var wpisDoUsuniecia = await _context.DietPlanDays.FindAsync(planDayId);

        if (wpisDoUsuniecia != null)
        {
            _context.DietPlanDays.Remove(wpisDoUsuniecia);
            await _context.SaveChangesAsync();
        }

        return RedirectToPage(new { id = currentDietId });
    }
}