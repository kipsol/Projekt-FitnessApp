using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApplication1.Models;
using WebApplication1.Repositories;
using WebApplication1.DTOs;

namespace WebApplication1.Pages.Exercises;

public class CreateModel : PageModel
{
    private static readonly string[] AllowedExtensions = [".jpg", ".jpeg", ".png", ".pdf"];
    private const long MaxFileSize = 5 * 1024 * 1024;
    private readonly ICwiczenieRepository _repository;
    private readonly IWebHostEnvironment _environment;

    public CreateModel(ICwiczenieRepository repository, IWebHostEnvironment environment)
    {
        _repository = repository;
        _environment = environment;
    }

    [BindProperty]
    public CwiczenieDto Cwiczenie { get; set; } = new();

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

        await _repository.AddAsync(new Cwiczenie
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
