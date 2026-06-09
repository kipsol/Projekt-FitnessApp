using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApplication1.DTOs;
using WebApplication1.Models;
using WebApplication1.Repositories;

namespace WebApplication1.Pages.Exercises;

public class EditModel : PageModel
{
    private static readonly string[] AllowedExtensions = [".jpg", ".jpeg", ".png", ".pdf"];
    private const long MaxFileSize = 5 * 1024 * 1024;
    private readonly ICwiczenieRepository _repository;
    private readonly IWebHostEnvironment _environment;

    public EditModel(ICwiczenieRepository repository, IWebHostEnvironment environment)
    {
        _repository = repository;
        _environment = environment;
    }

    [BindProperty]
    public CwiczenieDto Cwiczenie { get; set; } = null!;

    [BindProperty]
    public IFormFile? PlikCwiczenia { get; set; }

    public SelectList PartieMiesniowe { get; set; } = null!;

    public SelectList Maszyny { get; set; } = null!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        var cwiczenie = await _repository.GetByIdAsync(id.Value);

        if (cwiczenie is null)
        {
            return NotFound();
        }

        Cwiczenie = new CwiczenieDto
        {
            Id = cwiczenie.Id,
            Nazwa = cwiczenie.Nazwa,
            OpisWykonania = cwiczenie.OpisWykonania,
            PartiaMiesniowaId = cwiczenie.PartiaMiesniowaId,
            MaszynaId = cwiczenie.MaszynaId,
            LiczbaSerii = cwiczenie.LiczbaSerii,
            LiczbaPowtorzen = cwiczenie.LiczbaPowtorzen,
            PrzerwaSekundy = cwiczenie.PrzerwaSekundy,
            PlikSciezka = cwiczenie.PlikSciezka
        };

        await LoadListsAsync();
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            await LoadListsAsync();
            return Page();
        }

        var existing = await _repository.GetByIdAsync(Cwiczenie.Id);

        if (existing is null)
        {
            return NotFound();
        }

        Cwiczenie.PlikSciezka = existing.PlikSciezka;

        var filePath = await SaveExerciseFileAsync(PlikCwiczenia);

        if (!ModelState.IsValid)
        {
            await LoadListsAsync();
            return Page();
        }

        if (!string.IsNullOrWhiteSpace(filePath))
        {
            Cwiczenie.PlikSciezka = filePath;
        }

        var entity = new Cwiczenie
        {
            Id = Cwiczenie.Id,
            Nazwa = Cwiczenie.Nazwa,
            OpisWykonania = Cwiczenie.OpisWykonania,
            PartiaMiesniowaId = Cwiczenie.PartiaMiesniowaId,
            MaszynaId = Cwiczenie.MaszynaId,
            LiczbaSerii = Cwiczenie.LiczbaSerii,
            LiczbaPowtorzen = Cwiczenie.LiczbaPowtorzen,
            PrzerwaSekundy = Cwiczenie.PrzerwaSekundy,
            PlikSciezka = Cwiczenie.PlikSciezka
        };

        await _repository.UpdateAsync(entity);
        await _repository.SaveAsync();

        return RedirectToPage("./Index");
    }

    private async Task LoadListsAsync()
    {
        PartieMiesniowe = new SelectList(await _repository.GetAllPartieAsync(), "Id", "Nazwa");
        Maszyny = new SelectList(await _repository.GetAllMaszynyAsync(), "Id", "Nazwa");
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
}
