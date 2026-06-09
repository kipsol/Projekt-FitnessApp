using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApplication1.Models;
using WebApplication1.Repositories;
using WebApplication1.DTOs;

namespace WebApplication1.Pages.TrainingPlans;

public class CreateModel : PageModel
{
    private readonly IPlanTreningowyRepository _repository;

    public CreateModel(IPlanTreningowyRepository repository)
    {
        _repository = repository;
    }

    [BindProperty]
    public PlanTreningowyDto Plan { get; set; } = new();

    public SelectList Cwiczenia { get; set; } = null!;

    public async Task OnGetAsync()
    {
        await LoadCwiczeniaAsync();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            await LoadCwiczeniaAsync();
            return Page();
        }

        var plan = new PlanTreningowy
        {
            Nazwa = Plan.Nazwa,
            Opis = Plan.Opis,
            PoziomZaawansowania = Plan.PoziomZaawansowania,
            CzasTrwaniaTygodnie = Plan.CzasTrwaniaTygodnie
        };

        await _repository.AddAsync(plan);
        await _repository.SaveAsync();

        if (Plan.CwiczenieIds is { Count: > 0 })
        {
            var allCwiczenia = await _repository.GetAllCwiczeniaAsync();
            var selectedCwiczenia = allCwiczenia
                .Where(cwiczenie => Plan.CwiczenieIds.Contains(cwiczenie.Id))
                .ToList();

            foreach (var cwiczenie in selectedCwiczenia)
            {
                await _repository.AddPozycjaAsync(new PozycjaPlanu
                {
                    PlanTreningowyId = plan.Id,
                    CwiczenieId = cwiczenie.Id,
                    DzienTreningowy = "Do ustalenia",
                    LiczbaSerii = cwiczenie.LiczbaSerii,
                    LiczbaPowtorzen = cwiczenie.LiczbaPowtorzen,
                    PrzerwaSekundy = cwiczenie.PrzerwaSekundy
                });
            }

            await _repository.SaveAsync();
        }

        return RedirectToPage("./Index");
    }

    private async Task LoadCwiczeniaAsync()
    {
        var cwiczenia = await _repository.GetAllCwiczeniaAsync();
        Cwiczenia = new SelectList(cwiczenia, "Id", "Nazwa");
    }
}
