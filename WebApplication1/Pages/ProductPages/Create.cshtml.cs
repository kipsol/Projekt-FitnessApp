using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication1.DTOs;
using WebApplication1.Models;
using WebApplication1.Repositories;

namespace WebApplication1.Pages.ProductPages;

public class CreateModel : PageModel
{
    private readonly IProductRepository _repository;

    public CreateModel(IProductRepository repository)
    {
        _repository = repository;
    }

    public IActionResult OnGet() => Page();

    [BindProperty]
    public ProductDto Input { get; set; } = default!;

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid) return Page();

        var entity = new Product
        {
            Name = Input.Name,
            Description = Input.Description,
            Price = Input.Price,
            Stock = Input.Stock,
            Category = Input.Category
        };

        await _repository.AddAsync(entity);
        await _repository.SaveAsync();
        return RedirectToPage("./Index");
    }
}
