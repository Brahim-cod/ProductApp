using Microsoft.AspNetCore.Components;
using ProductWasm.Helpers;
using Shared.ModelsDto;

namespace ProductWasm.Pages.Checkout;

public partial class OrderConfirmatiom
{
    [Inject]
    private NavigationManager _navManager {  get; set; }
    [Inject]
    private CurrentOrder _currentOrderService { get; set; }
    private OrderProductDto _currentOrder {  get; set; }
    protected override async Task OnInitializedAsync()
    {
        if (_currentOrderService.CurrentOrderDto != null)
        {
            _currentOrder = _currentOrderService.CurrentOrderDto;
        }
        else
        {
            _navManager.NavigateTo("/cart");
        }
    }
}
