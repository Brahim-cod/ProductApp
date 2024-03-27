using Microsoft.AspNetCore.Components;
using Shared.ModelsDto;
using Shared.Services;

namespace ProductWasm.Pages;

public partial class Home
{
    [Inject]
    private ICategoryService _categoryService {  get; set; }
    public IEnumerable<CategoryDto> _categories { get; set; } = new List<CategoryDto>();
    [Inject]
    private NavigationManager _nav { get; set; }

    protected async override Task OnInitializedAsync()
    {
        var categories = await _categoryService.GetAll();
        if (categories.Any())
        {
            _categories = categories.Take(4);
        }
    }

}
