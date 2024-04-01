using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace ProductWasm;

public partial class App
{
    [CascadingParameter]
    private Task<AuthenticationState> authenticationStateTask {  get; set; }
    private async Task UserAuthenticationState()
    {
        var authState  = await authenticationStateTask;
        var user = authState.User;
        if (user.Identity.IsAuthenticated)
        {
            Console.WriteLine($"User {user.Identity.Name} is Authenticated.");

        }
        else
        {
            Console.WriteLine("Please log in to access this page.");
        }
    }
}
