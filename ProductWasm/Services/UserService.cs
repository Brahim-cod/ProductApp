using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using ProductWasm.Helpers;
using Shared.ModelsDto;
using Shered.ModelsDto;
using Shered.Services;
using System.Globalization;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace ProductWasm.Services;

public class UserService : IUserService
{
    private const string AUTHKEY = "authToken";
    private readonly HttpClient _http;
    private readonly JsonSerializerOptions _serializerOptions;
    private readonly AuthenticationStateProvider _authenticationStateProvider;
    public IJSRuntime JSRuntime { get; }
    public UserService(HttpClient http, IJSRuntime jSRuntime, AuthenticationStateProvider authenticationStateProvider)
    {
        _http = http;
        _serializerOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
        };
        JSRuntime = jSRuntime;
        _authenticationStateProvider = authenticationStateProvider;
    }
    public async Task<UserDto> Login(LoginDto login)
    {
        try
        {
            var loginJson = new StringContent(JsonSerializer.Serialize(login), Encoding.UTF8, "application/json");

            var response = await _http.PostAsync($"api/Account/login", loginJson);
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }
            var resBody = await response.Content.ReadAsStreamAsync();
            var user = await JsonSerializer.DeserializeAsync<UserDto>(resBody, _serializerOptions);

            await JSRuntime.InvokeVoidAsync("localStorage.setItem", AUTHKEY, user.Token);

            ((AuthStateProvider)_authenticationStateProvider).MarkUserAsAuthenticated(user.Email);
            _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", user.Token);

            return new UserDto();
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

            var response = await _http.PostAsync("api/Account/register", userJson);
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var resBody = await response.Content.ReadAsStreamAsync();
            var userRes = await JsonSerializer.DeserializeAsync<UserDto>(resBody, _serializerOptions);

            await JSRuntime.InvokeVoidAsync("localStorage.setItem", AUTHKEY, userRes.Token);

            ((AuthStateProvider)_authenticationStateProvider).MarkUserAsAuthenticated(userRes.Email);
            _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", userRes.Token);
            return userRes;
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error: {e}");
            throw;
        }
       
    }
    public async Task Logout()
    {
        await JSRuntime.InvokeVoidAsync("localStorage.removeItem", AUTHKEY);
        ((AuthStateProvider)_authenticationStateProvider).MarkUserAsLoggedOut();
        _http.DefaultRequestHeaders.Authorization = null;
    }
}
