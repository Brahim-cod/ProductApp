using Microsoft.AspNetCore.Components;
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
