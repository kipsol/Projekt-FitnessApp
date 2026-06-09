using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Pages.TrainingPlans;

public class EditModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public EditModel(ApplicationDbContext context)
    {
        _context = context;
    }

    [BindProperty]
    public PlanInput Plan { get; set; } = new();

    public MultiSelectList Cwiczenia { get; set; } = null!;

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

        Plan = new PlanInput
        {
            Id = plan.Id,
            Nazwa = plan.Nazwa,
            Opis = plan.Opis,
            PoziomZaawansowania = plan.PoziomZaawansowania,
            CzasTrwaniaTygodnie = plan.CzasTrwaniaTygodnie,
            CwiczenieIds = plan.PozycjePlanu.Select(item => item.CwiczenieId).ToList()
        };

        await LoadCwiczeniaAsync(Plan.CwiczenieIds);
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            await LoadCwiczeniaAsync(Plan.CwiczenieIds);
            return Page();
        }

        var plan = await _context.PlanyTreningowe
            .Include(item => item.PozycjePlanu)
            .FirstOrDefaultAsync(item => item.Id == Plan.Id);

        if (plan is null)
        {
            return NotFound();
        }

        plan.Nazwa = Plan.Nazwa;
        plan.Opis = Plan.Opis;
        plan.PoziomZaawansowania = Plan.PoziomZaawansowania;
        plan.CzasTrwaniaTygodnie = Plan.CzasTrwaniaTygodnie;

        _context.PozycjePlanu.RemoveRange(plan.PozycjePlanu);

        var selectedIds = Plan.CwiczenieIds ?? new List<int>();

        if (selectedIds.Count > 0)
        {
            var selectedCwiczenia = await _context.Cwiczenia
                .Where(item => selectedIds.Contains(item.Id))
                .ToListAsync();

            foreach (var cwiczenie in selectedCwiczenia)
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
        }

        await _context.SaveChangesAsync();
        return RedirectToPage("./Index");
    }

    private async Task LoadCwiczeniaAsync(List<int>? selectedIds)
    {
        var cwiczenia = await _context.Cwiczenia
            .OrderBy(item => item.Nazwa)
            .ToListAsync();

        Cwiczenia = new MultiSelectList(cwiczenia, "Id", "Nazwa", selectedIds ?? new List<int>());
    }

    public class PlanInput
    {
        public int Id { get; set; }

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
