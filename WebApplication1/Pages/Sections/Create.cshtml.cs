using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Pages.Sections;

public class CreateModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public CreateModel(ApplicationDbContext context)
    {
        _context = context;
    }

    [BindProperty]
    public SekcjaInput Sekcja { get; set; } = new();

    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        _context.Sekcje.Add(new Sekcja
        {
            Nazwa = Sekcja.Nazwa,
            Pietro = Sekcja.Pietro
        });

        await _context.SaveChangesAsync();
        return RedirectToPage("./Index");
    }

    public class SekcjaInput
    {
        [Display(Name = "Nazwa")]
        [Required(ErrorMessage = "Podaj nazwe sekcji.")]
        [StringLength(120)]
        public string Nazwa { get; set; } = string.Empty;

        [Display(Name = "Pietro")]
        [Range(-1, 20, ErrorMessage = "Podaj pietro od -1 do 20.")]
        public int Pietro { get; set; }
    }
}
