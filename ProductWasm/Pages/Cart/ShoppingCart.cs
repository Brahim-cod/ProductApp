using Microsoft.AspNetCore.Components;
using ProductWasm.Services.Abstract;
using Shared.ModelsDto;

namespace ProductWasm.Pages.Cart;

public partial class ShoppingCart
{
    [Inject]
    private ICartService _cartService {  get; set; }
    private List<(ProductDto, int)> products { get; set; } = new List<(ProductDto, int)>();
    private double SubTotal { get; set; } = 0;
    private double ChargeTotal { get; set; } = 0;
    private double Total { get; set; } = 0;

    protected override async Task OnInitializedAsync()
    {
        await GetContent();
    }
    private async Task GetContent()
    {
        var cartContents = await _cartService.GetCartContents();

        // Group products by their ID and count the occurrences
        var productCounts = cartContents
            .GroupBy(p => p.ProductID)
            .Select(g => (product: g.First(), count: g.Count()));

        // Convert the groupings into tuples and add them to the products list
        products = productCounts.Select(pc => (pc.product, pc.count)).OrderBy(p => p.product.ProductName).ToList();
        SubTotal = Math.Round(products.Sum(p => p.Item1.ProductPrice * p.Item2), 2);
        ChargeTotal = Math.Round((double)(products.Count * 15), 2);
        Total = Math.Round(SubTotal + ChargeTotal, 2);
    }

    private async Task IncrementQuantity(ProductDto product, int quantity)
    {
        await _cartService.UpdateQuantity(product, quantity);
        await GetContent();
    }

}
