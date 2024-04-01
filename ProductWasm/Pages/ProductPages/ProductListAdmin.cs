using Microsoft.AspNetCore.Components;
using Shared.ModelsDto;
using Shared.Services;

namespace ProductWasm.Pages.ProductPages;

public partial class ProductListAdmin
{
    [Inject]
    private IProductService _productService { get; set; }
    public List<ProductDto> _products { get; set; } = new List<ProductDto>();

    protected async override Task OnInitializedAsync()
    {
        _products = (await _productService.GetAll()).ToList();
    }
    private async Task DeleteClick(ProductDto item) 
    {
        await _productService.Remove(item.ProductID);
        _products.Remove(item);
    }
}
