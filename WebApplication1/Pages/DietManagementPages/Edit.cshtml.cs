using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication1.DTOs;
using WebApplication1.Models;
using WebApplication1.Repositories;

namespace WebApplication1.Pages.DietManagementPages;

public class EditModel : PageModel
{
    private readonly IDietRepository _repository;

    public EditModel(IDietRepository repository)
    {
        _repository = repository;
    }

    [BindProperty]
    public DietDto Input { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id is null) return NotFound();

        var entity = await _repository.GetByIdAsync(id.Value);
        if (entity is null) return NotFound();

        Input = new DietDto
        {
            Id = entity.Id,
            Name = entity.Name,
            Description = entity.Description,
            TargetCalories = entity.TargetCalories
        };

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid) return Page();

        var entity = new Diet
        {
            Id = Input.Id,
            Name = Input.Name,
            Description = Input.Description,
            TargetCalories = Input.TargetCalories
        };

        await _repository.UpdateAsync(entity);
        await _repository.SaveAsync();
        return RedirectToPage("./Index");
    }
}
