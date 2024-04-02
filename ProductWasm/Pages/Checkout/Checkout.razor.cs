using Microsoft.AspNetCore.Components;
using ProductWasm.Services.Abstract;
using Shared.ModelsDto;
using Shared.Services;

namespace ProductWasm.Pages.Checkout;

public partial class Checkout
{
    [Inject]
    private IOrderProductService _orderService { get; set; }
    [Inject]
    private ICartService _cartService { get; set; }
    [Inject]
    private NavigationManager _navigationManager { get; set; }
    private List<(ProductDto, int)> _products { get; set; } = new List<(ProductDto, int)>();
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
        _products = productCounts.Select(pc => (pc.product, pc.count)).OrderBy(p => p.product.ProductName).ToList();

        // Count and Calculate Amounts
        var SubTotal = Math.Round(_products.Sum(p => p.Item1.ProductPrice * p.Item2), 2);
        var ChargeTotal = Math.Round((double)(_products.Count * 15), 2);
        Total = Math.Round(SubTotal + ChargeTotal, 2);
    }
    private async Task CreateOrder()
    {
        var orders = _products.Select(p => new CreateOrderProductDto() { ProductId = p.Item1.ProductID, Quantity = p.Item2 });
        var order = await _orderService.CreateOrderProductAsync(orders);

        Console.WriteLine(order);
        await _cartService.RemoveAllFromCart();
        _navigationManager.NavigateTo("/");
    }
}
