using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Pages.DietPlanPages;

public class CreateModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public CreateModel(ApplicationDbContext context)
    {
        _context = context;
    }

    [BindProperty]
    public DietPlanDay DietPlanDay { get; set; } = default!;

    public SelectList DietyLista { get; set; } = default!;
    public SelectList PosilkiLista { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? dietId)
    {
        var diety = await _context.Diets.ToListAsync();
        var posilki = await _context.Meals.ToListAsync();

        int domyslnaDieta = dietId ?? (diety.FirstOrDefault()?.Id ?? 0);

        DietyLista = new SelectList(diety, "Id", "Name", domyslnaDieta);
        PosilkiLista = new SelectList(posilki, "Id", "Name");

        DietPlanDay = new DietPlanDay { DietId = domyslnaDieta };

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        ModelState.Remove("DietPlanDay.Diet");
        ModelState.Remove("DietPlanDay.Meal");

        if (!ModelState.IsValid)
        {
            var diety = await _context.Diets.ToListAsync();
            var posilki = await _context.Meals.ToListAsync();
            DietyLista = new SelectList(diety, "Id", "Name", DietPlanDay.DietId);
            PosilkiLista = new SelectList(posilki, "Id", "Name", DietPlanDay.MealId);
            return Page();
        }

        _context.DietPlanDays.Add(DietPlanDay);
        await _context.SaveChangesAsync();

        return RedirectToPage("/DietPages/Index", new { id = DietPlanDay.DietId });
    }
}