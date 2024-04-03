using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Shered.ModelsDto;
using Shered.Services;

namespace ProductWasm.Pages.UserPages;

public partial class Register
{
    [Inject]
    private IUserService userService {  get; set; }
    [Inject]
    private NavigationManager navigationManager { get; set; }

    private RegisterDto RegisterDto { get; set; } = new RegisterDto() { Role = "Customer" };

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
            navigationManager.NavigateTo("/");
        }
    }

    private async Task HandleValidSubmit()
    {
        var user = await userService.Register(RegisterDto);
        if (user != null)
        {
            navigationManager.Refresh();
        }
    }
}
