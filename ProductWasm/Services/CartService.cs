using Blazored.Toast.Services;
using Microsoft.JSInterop;
using ProductWasm.Services.Abstract;
using Shared.ModelsDto;
using System.Text.Json;

namespace ProductWasm.Services;

public class CartService : ICartService
{
    private const string CartKey = "cart";
    public IJSRuntime JSRuntime { get; }
    private readonly IToastService _toastService; // Inject IToastService



    // Inject IJSRuntime via constructor to access JavaScript APIs
    public CartService(IJSRuntime jsRuntime, IToastService toastService)
    {
        JSRuntime = jsRuntime;
        _toastService = toastService;
    }

    // Method to add a product to the cart
    public async Task AddToCart(ProductDto product)
    {
        var cart = await GetCartContents();
        cart.Add(product);
        await SaveCart(cart);
        _toastService.ShowToast(ToastLevel.Success, "<div class='shadow-[0_2px_10px_-3px_rgba(6,81,237,0.3)] text-black flex items-center border-l-4 border-green-500 w-max max-w-sm px-4 py-4 rounded' role='alert'><svg xmlns='http://www.w3.org/2000/svg' class='w-5 shrink-0 fill-green-500 inline mr-2' viewBox='0 0 512 512'><ellipse cx='256' cy='256' data-original='#000' rx='256' ry='255.832' /><path class='fill-white' d='m235.472 392.08-121.04-94.296 34.416-44.168 74.328 57.904 122.672-177.016 46.032 31.888z' data-original='#000' /></svg><span class='block sm:inline text-sm font-semibold'>Update successfully</span></div>");

    }

    // Method to remove a product from the cart
    public async Task RemoveFromCart(ProductDto product)
    {
        var cart = await GetCartContents();
        cart.RemoveAll(p => p.ProductID == product.ProductID);
        await SaveCart(cart);
    }

    // Method to update the quantity of a product in the cart
    public async Task UpdateQuantity(ProductDto product, int quantity)
    {
        var cart = await GetCartContents();
        cart.RemoveAll(p => p.ProductID == product.ProductID);
        for (int i = 0; i < quantity; i++)
        {
            cart.Add(product);
        }
        await SaveCart(cart);
    }

    // Method to retrieve the contents of the cart
    public async Task<List<ProductDto>?> GetCartContents()
    {
        var cartJson = await JSRuntime.InvokeAsync<string>("localStorage.getItem", CartKey);
        return string.IsNullOrEmpty(cartJson) ? new List<ProductDto>() : JsonSerializer.Deserialize<List<ProductDto>>(cartJson);
    }

    // Method to save the cart contents to local storage
    private async Task SaveCart(List<ProductDto> cart)
    {
        var cartJson = JsonSerializer.Serialize(cart);
        await JSRuntime.InvokeVoidAsync("localStorage.setItem", CartKey, cartJson);
    }

    public async Task RemoveAllFromCart()
    {
        await JSRuntime.InvokeVoidAsync("localStorage.removeItem", CartKey);
    }
}
