using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;
using WebApplication1.Security;

namespace WebApplication1.Pages.Trainer;

[Authorize(Policy = AppPolicies.AssignUserPlans)]
public class AssignmentsModel : PageModel
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<IdentityUser> _userManager;

    public AssignmentsModel(ApplicationDbContext context, UserManager<IdentityUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    public IList<UserAssignmentView> Users { get; set; } = new List<UserAssignmentView>();

    public IList<SelectListItem> TrainingPlans { get; set; } = new List<SelectListItem>();

    public IList<SelectListItem> Diets { get; set; } = new List<SelectListItem>();

    [BindProperty]
    public AssignmentInput Input { get; set; } = new();

    [TempData]
    public string? StatusMessage { get; set; }

    public async Task OnGetAsync()
    {
        await LoadPageDataAsync();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            await LoadPageDataAsync();
            return Page();
        }

        var user = await _userManager.FindByIdAsync(Input.UserId);

        if (user is null || !await _userManager.IsInRoleAsync(user, AppRoles.Klient))
        {
            ModelState.AddModelError(string.Empty, "Wybrany uzytkownik nie istnieje albo nie jest zwyklym klientem.");
            await LoadPageDataAsync();
            return Page();
        }

        if (Input.PlanTreningowyId.HasValue &&
            !await _context.PlanyTreningowe.AnyAsync(plan => plan.Id == Input.PlanTreningowyId.Value))
        {
            ModelState.AddModelError(string.Empty, "Wybrany plan treningowy nie istnieje.");
            await LoadPageDataAsync();
            return Page();
        }

        if (Input.DietId.HasValue &&
            !await _context.Diets.AnyAsync(diet => diet.Id == Input.DietId.Value))
        {
            ModelState.AddModelError(string.Empty, "Wybrana dieta nie istnieje.");
            await LoadPageDataAsync();
            return Page();
        }

        var trainerId = _userManager.GetUserId(User);
        if (trainerId is null)
        {
            return Challenge();
        }

        var assignment = await _context.UserFitnessAssignments
            .FirstOrDefaultAsync(existingAssignment => existingAssignment.UserId == Input.UserId);

        if (assignment is null)
        {
            assignment = new UserFitnessAssignment
            {
                UserId = Input.UserId
            };

            _context.UserFitnessAssignments.Add(assignment);
        }

        assignment.PlanTreningowyId = Input.PlanTreningowyId;
        assignment.DietId = Input.DietId;
        assignment.AssignedByTrainerId = trainerId;
        assignment.AssignedAtUtc = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        StatusMessage = "Przypisanie zostalo zapisane.";
        return RedirectToPage();
    }

    private async Task LoadPageDataAsync()
    {
        TrainingPlans = await _context.PlanyTreningowe
            .OrderBy(plan => plan.Nazwa)
            .Select(plan => new SelectListItem
            {
                Value = plan.Id.ToString(),
                Text = plan.Nazwa
            })
            .ToListAsync();

        Diets = await _context.Diets
            .OrderBy(diet => diet.Name)
            .Select(diet => new SelectListItem
            {
                Value = diet.Id.ToString(),
                Text = diet.Name
            })
            .ToListAsync();

        var clients = await _userManager.GetUsersInRoleAsync(AppRoles.Klient);
        var clientIds = clients.Select(user => user.Id).ToList();

        var assignments = await _context.UserFitnessAssignments
            .Include(assignment => assignment.PlanTreningowy)
            .Include(assignment => assignment.Diet)
            .Include(assignment => assignment.AssignedByTrainer)
            .Where(assignment => clientIds.Contains(assignment.UserId))
            .AsNoTracking()
            .ToDictionaryAsync(assignment => assignment.UserId);

        Users = clients
            .OrderBy(user => user.Email ?? user.UserName)
            .Select(user =>
            {
                assignments.TryGetValue(user.Id, out var assignment);

                return new UserAssignmentView
                {
                    UserId = user.Id,
                    Email = user.Email ?? user.UserName ?? user.Id,
                    PlanTreningowyId = assignment?.PlanTreningowyId,
                    PlanName = assignment?.PlanTreningowy?.Nazwa,
                    DietId = assignment?.DietId,
                    DietName = assignment?.Diet?.Name,
                    AssignedBy = assignment?.AssignedByTrainer.Email ?? assignment?.AssignedByTrainer.UserName,
                    AssignedAtUtc = assignment?.AssignedAtUtc
                };
            })
            .ToList();
    }

    public class AssignmentInput
    {
        [Required]
        public string UserId { get; set; } = string.Empty;

        [Display(Name = "Plan treningowy")]
        public int? PlanTreningowyId { get; set; }

        [Display(Name = "Dieta")]
        public int? DietId { get; set; }
    }

    public class UserAssignmentView
    {
        public string UserId { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public int? PlanTreningowyId { get; set; }

        public string? PlanName { get; set; }

        public int? DietId { get; set; }

        public string? DietName { get; set; }

        public string? AssignedBy { get; set; }

        public DateTime? AssignedAtUtc { get; set; }
    }
}
