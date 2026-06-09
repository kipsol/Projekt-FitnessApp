using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication1.Models;
using WebApplication1.Repositories;

namespace WebApplication1.Pages.ClassEventPages;

public class IndexModel : PageModel
{
    private readonly IClassEventRepository _repository;

    public IndexModel(IClassEventRepository repository)
    {
        _repository = repository;
    }

    public IList<ClassEvent> ClassEvent { get; set; } = default!;

    public async Task OnGetAsync()
    {
        ClassEvent = await _repository.GetAllAsync();
    }
}
