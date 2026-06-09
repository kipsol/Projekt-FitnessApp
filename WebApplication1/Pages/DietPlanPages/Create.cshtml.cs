using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApplication1.DTOs;
using WebApplication1.Models;
using WebApplication1.Repositories;

namespace WebApplication1.Pages.DietPlanPages;

public class CreateModel : PageModel
{
    private readonly IDietPlanDayRepository _repository;

    public CreateModel(IDietPlanDayRepository repository)
    {
        _repository = repository;
    }

    [BindProperty]
    public DietPlanDayDto Input { get; set; } = default!;

    public SelectList DietyLista { get; set; } = default!;
    public SelectList PosilkiLista { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? dietId)
    {
        var diety = await _repository.GetAllDietsAsync();
        var posilki = await _repository.GetAllMealsAsync();

        int domyslnaDieta = dietId ?? (diety.FirstOrDefault()?.Id ?? 0);

        DietyLista = new SelectList(diety, "Id", "Name", domyslnaDieta);
        PosilkiLista = new SelectList(posilki, "Id", "Name");

        Input = new DietPlanDayDto { DietId = domyslnaDieta };
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            var diety = await _repository.GetAllDietsAsync();
            var posilki = await _repository.GetAllMealsAsync();
            DietyLista = new SelectList(diety, "Id", "Name", Input.DietId);
            PosilkiLista = new SelectList(posilki, "Id", "Name", Input.MealId);
            return Page();
        }

        var entity = new DietPlanDay
        {
            DietId = Input.DietId,
            MealId = Input.MealId,
            Day = Input.Day,
            MealType = Input.MealType
        };

        await _repository.AddAsync(entity);
        await _repository.SaveAsync();
        return RedirectToPage("/DietPages/Index", new { id = Input.DietId });
    }
}
