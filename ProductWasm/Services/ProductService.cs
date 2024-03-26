using Microsoft.AspNetCore.Components;
using Shared.ModelsDto;
using Shared.Services;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace ProductWasm.Services;

public class ProductService : IProductService
{
    private readonly HttpClient _http;
    private readonly JsonSerializerOptions _serializerOptions;

    public ProductService(HttpClient http)
    {
        _http = http;
        _serializerOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
        };
    }

    public async Task<ProductDto?> Create(CreateProductDto entity)
    {
        try
        {
            var productJson = new StringContent(JsonSerializer.Serialize(entity), Encoding.UTF8, "application/json");

            var res = await _http.PostAsync("api/Product", productJson);

            if (!res.IsSuccessStatusCode)
            {
                return null;
            }

            var resBody = await res.Content.ReadAsStreamAsync();
            var newProduct = await JsonSerializer.DeserializeAsync<ProductDto>(resBody, _serializerOptions);
            return newProduct;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<IReadOnlyCollection<ProductDto>?> GetAll()
    {
        try
        {
            var apiResponse = await _http.GetStreamAsync("api/Product");

            var products = await JsonSerializer.DeserializeAsync<IReadOnlyCollection<ProductDto>>(apiResponse, _serializerOptions);
            return products;

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

    }

    public async Task<IReadOnlyCollection<ProductDto>?> GetAllByName(string name)
    {
        var apiResponse = await _http.GetStreamAsync($"api/Product/{name}");

        var products = await JsonSerializer.DeserializeAsync<IReadOnlyCollection<ProductDto>>(apiResponse, _serializerOptions);
        return products;
    }

    public async Task<ProductDto?> GetByID(int id)
    {
        try
        {
            var product = await _http.GetFromJsonAsync<ProductDto>($"api/Product/{id}", _serializerOptions);
            return product;
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error: {e}");
            throw;
        }
    }

    public async Task<bool> Remove(int id)
    {
        try
        {
            var res = await _http.DeleteAsync($"api/Product/{id}");
            return res.IsSuccessStatusCode;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }    
    }

    public async Task<bool> Update(UpdateProductDto entity)
    {
        try
        {
            var productJson = new StringContent(JsonSerializer.Serialize(entity), Encoding.UTF8, "application/json");

            var res = await _http.PutAsync($"api/Product", productJson);

            return res.IsSuccessStatusCode;
            
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}
