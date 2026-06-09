using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication1.DTOs;
using WebApplication1.Models;
using WebApplication1.Repositories;

namespace WebApplication1.Pages.MealPages;

public class CreateModel : PageModel
{
    private readonly IMealRepository _repository;

    public CreateModel(IMealRepository repository)
    {
        _repository = repository;
    }

    public IActionResult OnGet() => Page();

    [BindProperty]
    public MealDto Input { get; set; } = default!;

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid) return Page();

        var entity = new Meal
        {
            Name = Input.Name,
            Calories = Input.Calories,
            Description = Input.Description
        };

        await _repository.AddAsync(entity);
        await _repository.SaveAsync();
        return RedirectToPage("./Index");
    }
}
