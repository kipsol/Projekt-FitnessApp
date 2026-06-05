using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;
using WebApplication1.Data;

namespace WebApplication1.Pages.DietPages;

public class DietManagementIndexModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public DietManagementIndexModel(ApplicationDbContext context)
    {
        _context = context;
    }

    public IList<Diet> Diet { get; set; } = default!;

    public async Task OnGetAsync()
    {
        Diet = await _context.Diets.ToListAsync();
    }
}
