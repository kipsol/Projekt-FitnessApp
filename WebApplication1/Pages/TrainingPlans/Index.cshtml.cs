using System.Security.Claims;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;
using WebApplication1.Repositories;
using WebApplication1.Security;

namespace WebApplication1.Pages.TrainingPlans;

public class IndexModel : PageModel
{
    private readonly IPlanTreningowyRepository _repository;
    private readonly ApplicationDbContext _context;

    public IndexModel(IPlanTreningowyRepository repository, ApplicationDbContext context)
    {
        _repository = repository;
        _context = context;
    }

    public IList<PlanTreningowy> PlanyTreningowe { get; set; } = new List<PlanTreningowy>();

    public bool CanManagePlans => User.HasClaim(AppClaimTypes.Permission, AppPermissions.AssignUserPlans);

    public string? EmptyMessage { get; set; }

    public async Task OnGetAsync()
    {
        PlanyTreningowe = await _repository.GetAllAsync();

        if (CanManagePlans || User.Identity?.IsAuthenticated != true)
        {
            return;
        }

        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        var assignedPlanId = await _context.UserFitnessAssignments
            .AsNoTracking()
            .Where(assignment => assignment.UserId == userId)
            .Select(assignment => assignment.PlanTreningowyId)
            .FirstOrDefaultAsync();

        if (!assignedPlanId.HasValue)
        {
            PlanyTreningowe = new List<PlanTreningowy>();
            EmptyMessage = "Trener nie przypisal Ci jeszcze planu treningowego.";
            return;
        }

        PlanyTreningowe = PlanyTreningowe
            .Where(plan => plan.Id == assignedPlanId.Value)
            .ToList();
    }
}
