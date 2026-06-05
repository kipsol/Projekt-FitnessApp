using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Pages.Profiles;

[Authorize]
public class IndexModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public IndexModel(ApplicationDbContext context)
    {
        _context = context;
    }

    public UserProfile? UserProfile { get; set; }

    public string AccountName => User.Identity?.Name ?? "zalogowany uzytkownik";

    [BindProperty]
    public ProfileInput Input { get; set; } = new();

    [TempData]
    public string? StatusMessage { get; set; }

    public async Task<IActionResult> OnGetAsync()
    {
        var userId = GetUserId();
        if (userId is null)
        {
            return Challenge();
        }

        UserProfile = await _context.UserProfiles
            .AsNoTracking()
            .FirstOrDefaultAsync(profile => profile.UserId == userId);

        if (UserProfile is not null)
        {
            Input = new ProfileInput
            {
                FirstName = UserProfile.FirstName,
                LastName = UserProfile.LastName,
                Age = UserProfile.Age,
                HeightCm = UserProfile.HeightCm,
                WeightKg = UserProfile.WeightKg,
                Goal = UserProfile.Goal
            };
        }

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
            UserProfile = await _context.UserProfiles
                .AsNoTracking()
                .FirstOrDefaultAsync(profile => profile.UserId == userId);

            return Page();
        }

        var profile = await _context.UserProfiles
            .FirstOrDefaultAsync(existingProfile => existingProfile.UserId == userId);

        if (profile is null)
        {
            profile = new UserProfile
            {
                UserId = userId,
                CreatedAt = DateTime.UtcNow
            };

            _context.UserProfiles.Add(profile);
        }

        profile.FirstName = Input.FirstName;
        profile.LastName = Input.LastName;
        profile.Age = Input.Age;
        profile.HeightCm = Input.HeightCm;
        profile.WeightKg = Input.WeightKg;
        profile.Goal = Input.Goal;

        await _context.SaveChangesAsync();

        StatusMessage = "Profil zostal zapisany.";
        return RedirectToPage();
    }

    private string? GetUserId()
    {
        return User.FindFirstValue(ClaimTypes.NameIdentifier);
    }

    public class ProfileInput
    {
        [Required(ErrorMessage = "Imie jest wymagane.")]
        [MaxLength(80)]
        [Display(Name = "Imie")]
        public string FirstName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Nazwisko jest wymagane.")]
        [MaxLength(80)]
        [Display(Name = "Nazwisko")]
        public string LastName { get; set; } = string.Empty;

        [Range(1, 120)]
        [Display(Name = "Wiek")]
        public int Age { get; set; }

        [Range(50, 250)]
        [Display(Name = "Wzrost (cm)")]
        public decimal HeightCm { get; set; }

        [Range(20, 400)]
        [Display(Name = "Waga (kg)")]
        public decimal WeightKg { get; set; }

        [MaxLength(300)]
        [Display(Name = "Cel")]
        public string? Goal { get; set; }
    }
}
