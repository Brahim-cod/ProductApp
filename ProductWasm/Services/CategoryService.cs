using Shared.ModelsDto;
using Shared.Services;
using System.Net.Http.Json;
using System.Text.Json;

namespace ProductWasm.Services;

public class CategoryService : ICategoryService
{
    private readonly HttpClient _http;
    private readonly JsonSerializerOptions _serializerOptions;

    public CategoryService(HttpClient http)
    {
        _http = http;
        _serializerOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
        };
    }

    public async Task<IReadOnlyCollection<CategoryDto>?> GetAll()
    {
        try
        {
            var apiResponse = await _http.GetStreamAsync("api/Categories");

            var categories = await JsonSerializer.DeserializeAsync<IReadOnlyCollection<CategoryDto>>(apiResponse, _serializerOptions);
            return categories;

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<CategoryDto> GetByID(int id)
    {
        try
        {
            var category = await _http.GetFromJsonAsync<CategoryDto>($"api/Categories/{id}", _serializerOptions);
            return category;
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error: {e}");
            throw;
        }
    }
}
