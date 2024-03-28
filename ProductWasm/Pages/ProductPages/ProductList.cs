using Microsoft.AspNetCore.Components;
using ProductWasm.Extensions;
using Shared.ModelsDto;
using Shared.Services;

namespace ProductWasm.Pages.ProductPages;

public partial class ProductList
{
    [Inject]
    private IProductService _productService { get; set; }
    [Inject]
    private NavigationManager NavManager { get; set; }
    public IEnumerable<ProductDto> _products { get; set; } = new List<ProductDto>();

    protected async override Task OnInitializedAsync()
    {
        if (NavManager.TryGetQueryString<string>("name", out string name))
        {
            _products = await _productService.GetAllByName(name);
        }
        else
        {
            if (NavManager.TryGetQueryString<int>("categoryID", out int categoryID))
            {
                _products = await _productService.GetAllByCategory(categoryID);
            }
            else
            {
                _products = await _productService.GetAll();
            }
        }

    }
    private void RedirectToProductDetails(int productID)
    {
        NavManager.NavigateTo($"/productDetails/{productID}");
    }
}
