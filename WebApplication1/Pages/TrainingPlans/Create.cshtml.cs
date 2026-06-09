using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Pages.TrainingPlans;

public class CreateModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public CreateModel(ApplicationDbContext context)
    {
        _context = context;
    }

    [BindProperty]
    public PlanInput Plan { get; set; } = new();

    public SelectList Cwiczenia { get; set; } = null!;

    public async Task OnGetAsync()
    {
        await LoadCwiczeniaAsync();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            await LoadCwiczeniaAsync();
            return Page();
        }

        var plan = new PlanTreningowy
        {
            Nazwa = Plan.Nazwa,
            Opis = Plan.Opis,
            PoziomZaawansowania = Plan.PoziomZaawansowania,
            CzasTrwaniaTygodnie = Plan.CzasTrwaniaTygodnie
        };

        _context.PlanyTreningowe.Add(plan);
        await _context.SaveChangesAsync();

        if (Plan.CwiczenieIds is { Count: > 0 })
        {
            var cwiczenia = await _context.Cwiczenia
                .Where(cwiczenie => Plan.CwiczenieIds.Contains(cwiczenie.Id))
                .ToListAsync();

            foreach (var cwiczenie in cwiczenia)
            {
                _context.PozycjePlanu.Add(new PozycjaPlanu
                {
                    PlanTreningowyId = plan.Id,
                    CwiczenieId = cwiczenie.Id,
                    DzienTreningowy = "Do ustalenia",
                    LiczbaSerii = cwiczenie.LiczbaSerii,
                    LiczbaPowtorzen = cwiczenie.LiczbaPowtorzen,
                    PrzerwaSekundy = cwiczenie.PrzerwaSekundy
                });
            }

            await _context.SaveChangesAsync();
        }

        return RedirectToPage("./Index");
    }

    private async Task LoadCwiczeniaAsync()
    {
        var cwiczenia = await _context.Cwiczenia
            .OrderBy(cwiczenie => cwiczenie.Nazwa)
            .ToListAsync();

        Cwiczenia = new SelectList(cwiczenia, "Id", "Nazwa");
    }

    public class PlanInput
    {
        [Display(Name = "Nazwa")]
        [Required(ErrorMessage = "Podaj nazwe planu.")]
        [StringLength(120)]
        public string Nazwa { get; set; } = string.Empty;

        [Display(Name = "Opis")]
        [StringLength(1000)]
        public string? Opis { get; set; }

        [Display(Name = "Poziom zaawansowania")]
        [Required(ErrorMessage = "Wybierz poziom zaawansowania.")]
        [StringLength(40)]
        public string PoziomZaawansowania { get; set; } = string.Empty;

        [Display(Name = "Czas trwania w tygodniach")]
        [Range(1, 104, ErrorMessage = "Podaj czas od 1 do 104 tygodni.")]
        public int CzasTrwaniaTygodnie { get; set; } = 8;

        [Display(Name = "Cwiczenia w planie")]
        public List<int>? CwiczenieIds { get; set; }
    }
}
