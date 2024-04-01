using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Shared.ModelsDto;
using Shered.ModelsDto;
using Shered.Services;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace ProductWasm.Services;

public class UserService : IUserService
{
    private readonly HttpClient _http;
    private readonly JsonSerializerOptions _serializerOptions;

    public UserService(HttpClient http)
    {
        _http = http;
        _serializerOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
        };
    }
    public async Task<string> Login(LoginDto login)
    {
        try
        {
            var loginJson = new StringContent(JsonSerializer.Serialize(login), Encoding.UTF8, "application/json");

            var response = await _http.PostAsync($"api/Account/login", loginJson);
            response.EnsureSuccessStatusCode(); // Throw exception if response status code is not success
            var token = await response.Content.ReadAsStringAsync();
            return token;
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error: {e}");
            throw;
        }
    }

    public async Task<UserDto> Register(RegisterDto user)
    {
        try
        {
            var userJson = new StringContent(JsonSerializer.Serialize(user), Encoding.UTF8, "application/json");

            var response = await _http.PostAsync("api/user/register", userJson);
            response.EnsureSuccessStatusCode(); // Throw exception if response status code is not success

            var resBody = await response.Content.ReadAsStreamAsync();
            var userRes = await JsonSerializer.DeserializeAsync<UserDto>(resBody, _serializerOptions);
            return userRes;
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error: {e}");
            throw;
        }
       
    }
}
