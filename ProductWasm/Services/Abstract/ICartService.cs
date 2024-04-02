using Shared.ModelsDto;

namespace ProductWasm.Services.Abstract;

public interface ICartService
{
    Task AddToCart(ProductDto product);
    Task RemoveFromCart(ProductDto product);
    Task RemoveAllFromCart();
    Task UpdateQuantity(ProductDto product, int quantity);
    Task<List<ProductDto>?> GetCartContents();
}
