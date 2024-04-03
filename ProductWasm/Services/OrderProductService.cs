using Microsoft.JSInterop;
using Shared.ModelsDto;
using Shared.Services;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace ProductWasm.Services;

public class OrderProductService : IOrderProductService
{
    private readonly HttpClient _http;
    private readonly JsonSerializerOptions _serializerOptions;
    private readonly IJSRuntime _JSRuntime;

    public OrderProductService(HttpClient http, IJSRuntime jSRuntime)
    {
        _http = http;
        _serializerOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
        };
        _JSRuntime = jSRuntime;
    }
    private async Task AddAuthorization()
    {
        var token = await _JSRuntime.InvokeAsync<string>("localStorage.getItem", "authToken");

        _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
    }
    public async Task<OrderProductDto?> CreateOrderProductAsync(IEnumerable<CreateOrderProductDto> orderProducts)
    {
        try
        {
            var orderJson = new StringContent(JsonSerializer.Serialize(orderProducts), Encoding.UTF8, "application/json");

            AddAuthorization();

            var res = await _http.PostAsync("api/OrderProducts/create", orderJson);

            if (!res.IsSuccessStatusCode)
            {
                return null;
            }

            var resBody = await res.Content.ReadAsStreamAsync();
            var orderCreated = await JsonSerializer.DeserializeAsync<OrderProductDto>(resBody, _serializerOptions);
            return orderCreated;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<IReadOnlyCollection<OrderProductDto>?> GetAllOrderProductsAsync()
    {
        try
        {
            AddAuthorization();

            var apiResponse = await _http.GetStreamAsync("api/OrderProducts");

            var orders = await JsonSerializer.DeserializeAsync<IReadOnlyCollection<OrderProductDto>>(apiResponse, _serializerOptions);
            return orders;

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<OrderProductDto?> UpdateOrderProductAsync(int orderId, IEnumerable<CreateOrderProductDto> updatedOrderProducts)
    {
        try
        {
            var ordersJson = new StringContent(JsonSerializer.Serialize(updatedOrderProducts), Encoding.UTF8, "application/json");

            AddAuthorization();

            var res = await _http.PutAsync($"api/OrderProducts/update/{orderId}", ordersJson);

            if (!res.IsSuccessStatusCode)
            {
                return null;
            }

            var resBody = await res.Content.ReadAsStreamAsync();
            var orderUpdated = await JsonSerializer.DeserializeAsync<OrderProductDto>(resBody, _serializerOptions);
            return orderUpdated;

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}
