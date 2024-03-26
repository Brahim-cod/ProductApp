using Shared.ModelsDto;

namespace Shared.Services;

public interface IOrderProductService
{
    Task<IReadOnlyCollection<OrderProductDto>> GetAllOrderProductsAsync();
    Task<OrderProductDto> CreateOrderProductAsync(IEnumerable<CreateOrderProductDto> orderProducts);
    Task<OrderProductDto> UpdateOrderProductAsync(int orderId, IEnumerable<CreateOrderProductDto> updatedOrderProducts);
}