using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication1.DTOs;
using WebApplication1.Models;
using WebApplication1.Repositories;

namespace WebApplication1.Pages.DietManagementPages;

public class CreateModel : PageModel
{
    private readonly IDietRepository _repository;

    public CreateModel(IDietRepository repository)
    {
        _repository = repository;
    }

    public IActionResult OnGet() => Page();

    [BindProperty]
    public DietDto Input { get; set; } = default!;

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid) return Page();

        var entity = new Diet
        {
            Name = Input.Name,
            Description = Input.Description,
            TargetCalories = Input.TargetCalories
        };

        await _repository.AddAsync(entity);
        await _repository.SaveAsync();
        return RedirectToPage("/DietPages/Index");
    }
}
