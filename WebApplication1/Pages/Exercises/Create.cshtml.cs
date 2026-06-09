using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Pages.Exercises;

public class CreateModel : PageModel
{
    private static readonly string[] AllowedExtensions = [".jpg", ".jpeg", ".png", ".pdf"];
    private const long MaxFileSize = 5 * 1024 * 1024;
    private readonly ApplicationDbContext _context;
    private readonly IWebHostEnvironment _environment;

    public CreateModel(ApplicationDbContext context, IWebHostEnvironment environment)
    {
        _context = context;
        _environment = environment;
    }

    [BindProperty]
    public CwiczenieInput Cwiczenie { get; set; } = new();

    [BindProperty]
    public IFormFile? PlikCwiczenia { get; set; }

    public SelectList PartieMiesniowe { get; set; } = null!;

    public SelectList Maszyny { get; set; } = null!;

    public async Task OnGetAsync()
    {
        await LoadListsAsync();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            await LoadListsAsync();
            return Page();
        }

        var filePath = await SaveExerciseFileAsync(PlikCwiczenia);

        if (!ModelState.IsValid)
        {
            await LoadListsAsync();
            return Page();
        }

        _context.Cwiczenia.Add(new Cwiczenie
        {
            Nazwa = Cwiczenie.Nazwa,
            OpisWykonania = Cwiczenie.OpisWykonania,
            PartiaMiesniowaId = Cwiczenie.PartiaMiesniowaId,
            MaszynaId = Cwiczenie.MaszynaId,
            LiczbaSerii = Cwiczenie.LiczbaSerii,
            LiczbaPowtorzen = Cwiczenie.LiczbaPowtorzen,
            PrzerwaSekundy = Cwiczenie.PrzerwaSekundy,
            PlikSciezka = filePath
        });

        await _context.SaveChangesAsync();
        return RedirectToPage("./Index");
    }

    private async Task LoadListsAsync()
    {
        PartieMiesniowe = new SelectList(await _context.PartieMiesniowe.OrderBy(partia => partia.Nazwa).ToListAsync(), "Id", "Nazwa");
        Maszyny = new SelectList(await _context.Maszyny.OrderBy(maszyna => maszyna.Nazwa).ToListAsync(), "Id", "Nazwa");
    }

    private async Task<string?> SaveExerciseFileAsync(IFormFile? file)
    {
        if (file is null || file.Length == 0)
        {
            return null;
        }

        if (file.Length > MaxFileSize)
        {
            ModelState.AddModelError(nameof(PlikCwiczenia), "Plik moze miec maksymalnie 5 MB.");
            return null;
        }

        var extension = Path.GetExtension(file.FileName).ToLowerInvariant();

        if (!AllowedExtensions.Contains(extension))
        {
            ModelState.AddModelError(nameof(PlikCwiczenia), "Dozwolone formaty: JPG, PNG lub PDF.");
            return null;
        }

        var uploadsPath = Path.Combine(_environment.WebRootPath, "uploads", "exercises");
        Directory.CreateDirectory(uploadsPath);

        var fileName = $"{Guid.NewGuid():N}{extension}";
        var physicalPath = Path.Combine(uploadsPath, fileName);

        await using var stream = System.IO.File.Create(physicalPath);
        await file.CopyToAsync(stream);

        return $"/uploads/exercises/{fileName}";
    }

    public class CwiczenieInput
    {
        [Display(Name = "Nazwa")]
        [Required(ErrorMessage = "Podaj nazwe cwiczenia.")]
        [StringLength(120)]
        public string Nazwa { get; set; } = string.Empty;

        [Display(Name = "Opis wykonania")]
        [Required(ErrorMessage = "Podaj opis wykonania.")]
        [StringLength(2000)]
        public string OpisWykonania { get; set; } = string.Empty;

        [Display(Name = "Partia miesniowa")]
        [Range(1, int.MaxValue, ErrorMessage = "Wybierz partie miesniowa.")]
        public int PartiaMiesniowaId { get; set; }

        [Display(Name = "Maszyna")]
        public int? MaszynaId { get; set; }

        [Display(Name = "Liczba serii")]
        [Range(1, 50, ErrorMessage = "Podaj liczbe serii od 1 do 50.")]
        public int LiczbaSerii { get; set; } = 4;

        [Display(Name = "Liczba powtorzen")]
        [Required(ErrorMessage = "Podaj liczbe powtorzen.")]
        [StringLength(30)]
        public string LiczbaPowtorzen { get; set; } = string.Empty;

        [Display(Name = "Przerwa w sekundach")]
        [Range(0, 3600, ErrorMessage = "Podaj przerwe od 0 do 3600 sekund.")]
        public int PrzerwaSekundy { get; set; } = 90;
    }
}
