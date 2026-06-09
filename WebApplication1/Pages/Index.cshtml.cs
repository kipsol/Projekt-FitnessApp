using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApplication1.Pages;

public class IndexModel : PageModel
{
    public IActionResult OnGet()
        => RedirectToPage("/TrainingPlans/Panel");
}
