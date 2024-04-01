using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components;
using ProductWasm.Helpers;
using System.Security.Claims;
using System.Data;

namespace ProductWasm.Layout;

public partial class NavMenu
{
    [Inject]
    private AuthenticationStateProvider _authenticationStateProvider { get; set; }
    private string userEmail;
    private string userName;
    private string userGivenName;
    protected override async Task OnInitializedAsync()
    {
        //var userClaims = new[]
        //{
        //    new Claim(ClaimTypes.NameIdentifier, user.Id),
        //    new Claim(ClaimTypes.Name, user.UserName),
        //    new Claim(ClaimTypes.GivenName, $"{user.FirstName} {user.LastName}"),
        //    new Claim(ClaimTypes.Email, user.Email),
        //    new Claim(ClaimTypes.Role, role)
        //};


        var authState = await _authenticationStateProvider.GetAuthenticationStateAsync();
        var user = authState.User;

        if (user.Identity.IsAuthenticated)
        {
            var emailClaim = user.FindFirst(c => c.Type == ClaimTypes.Email);
            userEmail = emailClaim?.Value ?? "Email not found";

            var nameClaim = user.FindFirst(c => c.Type == ClaimTypes.Name);
            userName = nameClaim?.Value ?? "Name not found";

            var givenNameClaim = user.FindFirst(c => c.Type == ClaimTypes.GivenName);
            userGivenName = givenNameClaim?.Value ?? "Given name not found";
        }
        else
        {
            userEmail = "Not authenticated";
            userName = "Not authenticated";
            userGivenName = "Not authenticated";
        }
    }

}
