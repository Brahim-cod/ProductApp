using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Shered.ModelsDto;
using Shered.Services;

namespace ProductWasm.Pages.UserPages;

public partial class Login
{
    [Inject]
    private IUserService _userService {  get; set; }
    private LoginDto LoginDto { get; set; } = new LoginDto();
    [Inject]
    private NavigationManager _navManager {  get; set; }
    [Inject]
    private AuthenticationStateProvider _authenticationStateProvider { get; set; }
    protected async override void OnInitialized()
    {
        // Check if the user is already authenticated
        var authState = await _authenticationStateProvider.GetAuthenticationStateAsync();
        var isAuthenticated = authState.User.Identity.IsAuthenticated;

        // If user is authenticated, redirect to homepage ("/")
        if (isAuthenticated)
        {
            _navManager.NavigateTo("/");
        }
    }
    private async Task HandleValidSubmit()
    {
        var res = await _userService.Login(LoginDto);
        if (res != null)
        {
            _navManager.NavigateTo("/");
        }
        else
        {
            _navManager.Refresh();
        }
    }
}
