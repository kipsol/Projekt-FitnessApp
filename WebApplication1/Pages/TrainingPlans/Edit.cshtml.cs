using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApplication1.Models;
using WebApplication1.Repositories;
using WebApplication1.DTOs;

namespace WebApplication1.Pages.TrainingPlans;

public class EditModel : PageModel
{
    private readonly IPlanTreningowyRepository _repository;

    public EditModel(IPlanTreningowyRepository repository)
    {
        _repository = repository;
    }

    [BindProperty]
    public PlanTreningowyDto Plan { get; set; } = new();

    public MultiSelectList Cwiczenia { get; set; } = null!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        var plan = await _repository.GetByIdWithDetailsAsync(id.Value);

        if (plan is null)
        {
            return NotFound();
        }

        Plan = new PlanTreningowyDto
        {
            Id = plan.Id,
            Nazwa = plan.Nazwa,
            Opis = plan.Opis,
            PoziomZaawansowania = plan.PoziomZaawansowania,
            CzasTrwaniaTygodnie = plan.CzasTrwaniaTygodnie,
            CwiczenieIds = plan.PozycjePlanu.Select(item => item.CwiczenieId).ToList()
        };

        await LoadCwiczeniaAsync(Plan.CwiczenieIds);
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            await LoadCwiczeniaAsync(Plan.CwiczenieIds);
            return Page();
        }

        var plan = await _repository.GetByIdWithDetailsAsync(Plan.Id);

        if (plan is null)
        {
            return NotFound();
        }

        plan.Nazwa = Plan.Nazwa;
        plan.Opis = Plan.Opis;
        plan.PoziomZaawansowania = Plan.PoziomZaawansowania;
        plan.CzasTrwaniaTygodnie = Plan.CzasTrwaniaTygodnie;

        foreach (var pozycja in plan.PozycjePlanu.ToList())
        {
            await _repository.DeletePozycjaAsync(pozycja.Id);
        }

        var selectedIds = Plan.CwiczenieIds ?? new List<int>();

        if (selectedIds.Count > 0)
        {
            var allCwiczenia = await _repository.GetAllCwiczeniaAsync();
            var selectedCwiczenia = allCwiczenia
                .Where(item => selectedIds.Contains(item.Id))
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
        }

        await _repository.SaveAsync();
        return RedirectToPage("./Index");
    }

    private async Task LoadCwiczeniaAsync(List<int>? selectedIds)
    {
        var cwiczenia = await _repository.GetAllCwiczeniaAsync();
        Cwiczenia = new MultiSelectList(cwiczenia, "Id", "Nazwa", selectedIds ?? new List<int>());
    }
}
