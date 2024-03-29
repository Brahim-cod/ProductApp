using Microsoft.AspNetCore.Components;
using Shared.Services;

namespace ProductWasm.Pages.ProductPages;

public partial class ProductCreate
{
    [Inject]
    private IProductService _productService {  get; set; }
    [Inject]
    private ICategoryService _categoryService { get; set; }

    [Parameter]
    public int? productID {  get; set; }
    

    protected override async Task OnInitializedAsync()
    {
        
    }
}
