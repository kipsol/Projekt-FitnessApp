using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Pages.Exercises;

public class EditModel : PageModel
{
    private static readonly string[] AllowedExtensions = [".jpg", ".jpeg", ".png", ".pdf"];
    private const long MaxFileSize = 5 * 1024 * 1024;
    private readonly ApplicationDbContext _context;
    private readonly IWebHostEnvironment _environment;

    public EditModel(ApplicationDbContext context, IWebHostEnvironment environment)
    {
        _context = context;
        _environment = environment;
    }

    [BindProperty]
    public Cwiczenie Cwiczenie { get; set; } = null!;

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

        var cwiczenie = await _context.Cwiczenia.FindAsync(id);

        if (cwiczenie is null)
        {
            return NotFound();
        }

        Cwiczenie = cwiczenie;
        await LoadListsAsync();
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        ModelState.Remove("Cwiczenie.PartiaMiesniowa");
        ModelState.Remove("Cwiczenie.Maszyna");
        ModelState.Remove("Cwiczenie.PozycjePlanu");

        if (!ModelState.IsValid)
        {
            await LoadListsAsync();
            return Page();
        }

        var existing = await _context.Cwiczenia.AsNoTracking().FirstOrDefaultAsync(item => item.Id == Cwiczenie.Id);

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

        _context.Attach(Cwiczenie).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!await _context.Cwiczenia.AnyAsync(item => item.Id == Cwiczenie.Id))
            {
                return NotFound();
            }

            throw;
        }

        return RedirectToPage("./Index");
    }

    private async Task LoadListsAsync()
    {
        PartieMiesniowe = new SelectList(await _context.PartieMiesniowe.OrderBy(item => item.Nazwa).ToListAsync(), "Id", "Nazwa");
        Maszyny = new SelectList(await _context.Maszyny.OrderBy(item => item.Nazwa).ToListAsync(), "Id", "Nazwa");
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
