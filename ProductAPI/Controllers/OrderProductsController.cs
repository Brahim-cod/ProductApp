using Microsoft.AspNetCore.Mvc;
using Repository.Models;
using Services.ModelsDto;
using Services.Services;

namespace ProductAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrderProductsController : ControllerBase
{
    private readonly IOrderProductService _orderProductService;

    public OrderProductsController(IOrderProductService orderProductService)
    {
        _orderProductService = orderProductService;
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyCollection<OrderProductDto>>> GetAllOrderProduct()
    {
        try
        {
            var orderProduct = await _orderProductService.GetAllOrderProductsAsync();
            return Ok(orderProduct);
        }
        catch (Exception ex)
        {
            return BadRequest($"An error occurred while creating order product: {ex.Message}");
        }
    }

    [HttpPost("create")]
    public async Task<ActionResult<OrderProductDto>> CreateOrderProductAsync(IEnumerable<CreateOrderProductDto> orderProducts)
    {
        try
        {
            var orderProduct = await _orderProductService.CreateOrderProductAsync(orderProducts);
            return Ok(orderProduct);
        }
        catch (Exception ex)
        {
            return BadRequest($"An error occurred while creating order product: {ex.Message}");
        }
    }

    [HttpPut("update/{orderId}")]
    public async Task<ActionResult<OrderProductDto>> UpdateOrderProductAsync(int orderId, IEnumerable<CreateOrderProductDto> updatedOrderProducts)
    {
        try
        {
            var orderProduct = await _orderProductService.UpdateOrderProductAsync(orderId, updatedOrderProducts);
            return Ok(orderProduct);
        }
        catch (Exception ex)
        {
            return BadRequest($"An error occurred while updating order product: {ex.Message}");
        }
    }

}