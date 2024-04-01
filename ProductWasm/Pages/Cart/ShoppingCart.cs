using Microsoft.AspNetCore.Components;
using ProductWasm.Services.Abstract;
using Shared.ModelsDto;

namespace ProductWasm.Pages.Cart;

public partial class ShoppingCart
{
    [Inject]
    private ICartService _cartService {  get; set; }
    private List<(ProductDto, int)> products { get; set; } = new List<(ProductDto, int)>();

    protected override async Task OnInitializedAsync()
    {
        var cartContents = await _cartService.GetCartContents();

        // Group products by their ID and count the occurrences
        var productCounts = cartContents
            .GroupBy(p => p.ProductID)
            .Select(g => (product: g.First(), count: g.Count()));

        // Convert the groupings into tuples and add them to the products list
        products = productCounts.Select(pc => (pc.product, pc.count)).ToList();
    }

}
