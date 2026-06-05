using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Pages.GymMachines;

public class CreateModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public CreateModel(ApplicationDbContext context)
    {
        _context = context;
    }

    [BindProperty]
    public MaszynaInput Maszyna { get; set; } = new();

    public SelectList Sekcje { get; set; } = null!;

    public async Task OnGetAsync()
    {
        await LoadSekcjeAsync();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            await LoadSekcjeAsync();
            return Page();
        }

        _context.Maszyny.Add(new Maszyna
        {
            Nazwa = Maszyna.Nazwa,
            Status = Maszyna.Status,
            SekcjaId = Maszyna.SekcjaId
        });

        await _context.SaveChangesAsync();
        return RedirectToPage("/Index");
    }

    private async Task LoadSekcjeAsync()
    {
        var sekcje = await _context.Sekcje
            .OrderBy(sekcja => sekcja.Nazwa)
            .ToListAsync();

        Sekcje = new SelectList(sekcje, "Id", "Nazwa");
    }

    public class MaszynaInput
    {
        [Display(Name = "Nazwa")]
        [Required(ErrorMessage = "Podaj nazwe maszyny.")]
        [StringLength(120)]
        public string Nazwa { get; set; } = string.Empty;

        [Display(Name = "Status")]
        [Required(ErrorMessage = "Wybierz status.")]
        [StringLength(40)]
        public string Status { get; set; } = string.Empty;

        [Display(Name = "Sekcja")]
        [Range(1, int.MaxValue, ErrorMessage = "Wybierz sekcje.")]
        public int SekcjaId { get; set; }
    }
}
