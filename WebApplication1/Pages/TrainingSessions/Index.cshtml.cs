using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Pages.TrainingSessions;

[Authorize]
public class IndexModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public IndexModel(ApplicationDbContext context)
    {
        _context = context;
    }

    public IList<TrainingSession> Sessions { get; set; } = new List<TrainingSession>();

    [BindProperty]
    public TrainingSessionInput Input { get; set; } = new();

    [TempData]
    public string? StatusMessage { get; set; }

    public async Task<IActionResult> OnGetAsync()
    {
        var userId = GetUserId();
        if (userId is null)
        {
            return Challenge();
        }

        Input = new TrainingSessionInput
        {
            SessionDate = DateTime.Now,
            DurationMinutes = 60
        };

        await LoadSessionsAsync(userId);
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
            await LoadSessionsAsync(userId);
            return Page();
        }

        var session = new TrainingSession
        {
            UserId = userId,
            Name = Input.Name,
            TrainingType = Input.TrainingType,
            SessionDate = Input.SessionDate,
            DurationMinutes = Input.DurationMinutes,
            CaloriesBurned = Input.CaloriesBurned,
            Notes = Input.Notes
        };

        _context.TrainingSessions.Add(session);
        await _context.SaveChangesAsync();

        StatusMessage = "Sesja zostala dodana.";
        return RedirectToPage();
    }

    public async Task<IActionResult> OnPostDeleteAsync(int id)
    {
        var userId = GetUserId();
        if (userId is null)
        {
            return Challenge();
        }

        var session = await _context.TrainingSessions
            .FirstOrDefaultAsync(existingSession => existingSession.Id == id && existingSession.UserId == userId);

        if (session is not null)
        {
            _context.TrainingSessions.Remove(session);
            await _context.SaveChangesAsync();
            StatusMessage = "Sesja zostala usunieta.";
        }

        return RedirectToPage();
    }

    private async Task LoadSessionsAsync(string userId)
    {
        Sessions = await _context.TrainingSessions
            .AsNoTracking()
            .Where(session => session.UserId == userId)
            .OrderByDescending(session => session.SessionDate)
            .ToListAsync();
    }

    private string? GetUserId()
    {
        return User.FindFirstValue(ClaimTypes.NameIdentifier);
    }

    public class TrainingSessionInput
    {
        [Required(ErrorMessage = "Nazwa sesji jest wymagana.")]
        [MaxLength(120)]
        [Display(Name = "Nazwa")]
        public string Name { get; set; } = string.Empty;

        [MaxLength(80)]
        [Display(Name = "Typ treningu")]
        public string? TrainingType { get; set; }

        [Display(Name = "Data sesji")]
        public DateTime SessionDate { get; set; } = DateTime.Now;

        [Range(1, 600)]
        [Display(Name = "Czas (min)")]
        public int DurationMinutes { get; set; } = 60;

        [Range(0, 5000)]
        [Display(Name = "Kalorie")]
        public int CaloriesBurned { get; set; }

        [MaxLength(500)]
        [Display(Name = "Notatki")]
        public string? Notes { get; set; }
    }
}
