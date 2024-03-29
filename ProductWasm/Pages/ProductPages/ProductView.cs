using Microsoft.AspNetCore.Components;
using ProductWasm.Services;
using ProductWasm.Services.Abstract;
using Shared.ModelsDto;
using Shared.Services;

namespace ProductWasm.Pages.ProductPages;

public partial class ProductView
{
    [Parameter]
    public int? ProductID { get; set; }
    [Inject]
    private IProductService _productService {  get; set; }
    [Inject]
    private ICartService _cartService { get; set; }

    public ProductDto Product { get; set; } = null;

    string disabledCursor = "cursor-not-allowed opacity-50";


    protected async override Task OnInitializedAsync()
    {
        if (ProductID.HasValue)
        {
            var product = await _productService.GetByID(ProductID.Value);
            if (product != null)
            {
                Product = product;
            }
        }
    }

    private void AddToCart()
    {
        // Add logic to add the selected product to the cart
        // For demonstration, let's assume there's a CartService to handle cart operations
        _cartService.AddToCart(Product);
    }
}
