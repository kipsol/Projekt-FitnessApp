using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication1.Models;
using WebApplication1.Repositories;

namespace WebApplication1.Pages.DietPages;

public class DietManagementIndexModel : PageModel
{
    private readonly IDietRepository _repository;

    public DietManagementIndexModel(IDietRepository repository)
    {
        _repository = repository;
    }

    public IList<Diet> Diet { get; set; } = default!;

    public async Task OnGetAsync()
    {
        Diet = await _repository.GetAllAsync();
    }
}
