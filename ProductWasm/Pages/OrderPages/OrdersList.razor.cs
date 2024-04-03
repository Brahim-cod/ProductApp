using Microsoft.AspNetCore.Components;
using Shared.ModelsDto;
using Shared.Services;

namespace ProductWasm.Pages.OrderPages;

public partial class OrdersList
{
    [Inject]
    private IOrderProductService _orderService {  get; set; }
    private IEnumerable<OrderProductDto> Orders { get; set; } = Enumerable.Empty<OrderProductDto>();

    protected override async Task OnInitializedAsync()
    {
        var orders = await _orderService.GetAllOrderProductsAsync();
        if (orders.Any())
        {
            Orders = orders;
        }
    }
}
