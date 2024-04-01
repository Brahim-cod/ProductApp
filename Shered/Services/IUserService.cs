using Shered.ModelsDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shered.Services;

public interface IUserService
{
    Task<UserDto> Register(RegisterDto user);
    Task<string> Login(LoginDto login);
}
