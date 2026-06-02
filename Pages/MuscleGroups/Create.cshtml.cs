using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Pages.MuscleGroups;

public class CreateModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public CreateModel(ApplicationDbContext context)
    {
        _context = context;
    }

    [BindProperty]
    public PartiaInput Partia { get; set; } = new();

    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        _context.PartieMiesniowe.Add(new PartiaMiesniowa
        {
            Nazwa = Partia.Nazwa
        });

        await _context.SaveChangesAsync();
        return RedirectToPage("/Index");
    }

    public class PartiaInput
    {
        [Display(Name = "Nazwa")]
        [Required(ErrorMessage = "Podaj nazwe partii miesniowej.")]
        [StringLength(120)]
        public string Nazwa { get; set; } = string.Empty;
    }
}
