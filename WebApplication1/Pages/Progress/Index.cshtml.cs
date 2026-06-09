using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Pages.Progress;

[Authorize]
public class IndexModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public IndexModel(ApplicationDbContext context)
    {
        _context = context;
    }

    public IList<ProgressEntry> Entries { get; set; } = new List<ProgressEntry>();

    [BindProperty]
    public ProgressInput Input { get; set; } = new();

    [TempData]
    public string? StatusMessage { get; set; }

    public async Task<IActionResult> OnGetAsync()
    {
        var userId = GetUserId();
        if (userId is null)
        {
            return Challenge();
        }

        Input = new ProgressInput
        {
            EntryDate = DateTime.Today
        };

        await LoadEntriesAsync(userId);
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        var userId = GetUserId();
        if (userId is null)
        {
            return Challenge();
        }

        if (!ModelState.IsValid)
        {
            await LoadEntriesAsync(userId);
            return Page();
        }

        var entry = new ProgressEntry
        {
            UserId = userId,
            EntryDate = Input.EntryDate,
            WeightKg = Input.WeightKg,
            ChestCm = Input.ChestCm,
            WaistCm = Input.WaistCm,
            HipCm = Input.HipCm,
            Notes = Input.Notes
        };

        _context.ProgressEntries.Add(entry);
        await _context.SaveChangesAsync();

        StatusMessage = "Wpis progresu zostal dodany.";
        return RedirectToPage();
    }

    public async Task<IActionResult> OnPostDeleteAsync(int id)
    {
        var userId = GetUserId();
        if (userId is null)
        {
            return Challenge();
        }

        var entry = await _context.ProgressEntries
            .FirstOrDefaultAsync(existingEntry => existingEntry.Id == id && existingEntry.UserId == userId);

        if (entry is not null)
        {
            _context.ProgressEntries.Remove(entry);
            await _context.SaveChangesAsync();
            StatusMessage = "Wpis progresu zostal usuniety.";
        }

        return RedirectToPage();
    }

    private async Task LoadEntriesAsync(string userId)
    {
        Entries = await _context.ProgressEntries
            .AsNoTracking()
            .Where(entry => entry.UserId == userId)
            .OrderByDescending(entry => entry.EntryDate)
            .ToListAsync();
    }

    private string? GetUserId()
    {
        return User.FindFirstValue(ClaimTypes.NameIdentifier);
    }

    public class ProgressInput
    {
        [Display(Name = "Data wpisu")]
        public DateTime EntryDate { get; set; } = DateTime.Today;

        [Range(20, 400)]
        [Display(Name = "Waga (kg)")]
        public decimal WeightKg { get; set; }

        [Range(0, 300)]
        [Display(Name = "Klatka (cm)")]
        public decimal? ChestCm { get; set; }

        [Range(0, 300)]
        [Display(Name = "Talia (cm)")]
        public decimal? WaistCm { get; set; }

        [Range(0, 300)]
        [Display(Name = "Biodra (cm)")]
        public decimal? HipCm { get; set; }

        [MaxLength(500)]
        [Display(Name = "Notatki")]
        public string? Notes { get; set; }
    }
}
