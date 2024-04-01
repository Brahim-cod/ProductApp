using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Repository.Models;
using Shered.ModelsDto;
using Shered.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services;

public class UserService : IUserService
{
    private readonly UserManager<AppUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IConfiguration _configuration;
    private readonly IMapper _mapper;
    public UserService(IConfiguration configuration, UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, IMapper mapper)
    {
        _configuration = configuration;
        _userManager = userManager;
        _roleManager = roleManager;
        _mapper = mapper;
    }

    public async Task<UserDto> Login(LoginDto login)
    {
        var getUser = await _userManager.FindByEmailAsync(login.Email);
        if (getUser is null)
        {
            throw new Exception("User Not Found");
        }
        bool checkPassword = await _userManager.CheckPasswordAsync(getUser, login.Password);
        if (!checkPassword)
        {
            throw new Exception("Invalid Email/Password");
        }
        var getRoles = await _userManager.GetRolesAsync(getUser);
        var token = GenerateToken(getUser, getRoles.First());



        return MapToUserDto(getUser, token, getRoles.First());

    }

    public async Task<UserDto> Register(RegisterDto userDto)
    {
        await ValidateUserRegistration(userDto);

        var newUser = await CreateUserAsync(userDto);
        var token = GenerateToken(newUser, userDto.Role);

        return MapToUserDto(newUser, token, userDto.Role);
    }

    private async Task ValidateUserRegistration(RegisterDto userDto)
    {
        // Check if a user with the same email already exists
        var existingUser = await _userManager.FindByEmailAsync(userDto.Email);
        if (existingUser != null)
        {
            throw new Exception("Email is already registered");
        }
    }

    private async Task<AppUser> CreateUserAsync(RegisterDto userDto)
    {
        var newUser = _mapper.Map<AppUser>(userDto);

        var result = await _userManager.CreateAsync(newUser, userDto.Password);
        if (!result.Succeeded)
        {
            throw new Exception($"Failed to create user: {string.Join("\n", result.Errors.Select(e => e.Description))}");
        }

        // Assign user to default role
        await AssignUserToDefaultRole(newUser);

        return newUser;
    }

    private async Task AssignUserToDefaultRole(AppUser user)
    {
        var defaultRoleName = "Customer"; // Default role for regular users
        var adminRoleName = "Admin"; // Role for the first user

        // Check if the admin role is in the system
        var isAdminNotExists = (await _roleManager.FindByNameAsync(adminRoleName)) == null;

        // Assign the appropriate role based on whether it's the first user or not
        var roleName = isAdminNotExists ? adminRoleName : defaultRoleName;

        // Check if the role exists, create it if not
        if (!await _roleManager.RoleExistsAsync(roleName))
        {
            var role = new IdentityRole(roleName);
            var createRoleResult = await _roleManager.CreateAsync(role);
            if (!createRoleResult.Succeeded)
            {
                throw new Exception($"Failed to create role '{roleName}': {string.Join("\n", createRoleResult.Errors.Select(e => e.Description))}");
            }
        }

        // Assign the user to the role
        var addToRoleResult = await _userManager.AddToRoleAsync(user, roleName);
        if (!addToRoleResult.Succeeded)
        {
            throw new Exception($"Failed to assign user to role '{roleName}': {string.Join("\n", addToRoleResult.Errors.Select(e => e.Description))}");
        }
    }

    private string GenerateToken(AppUser user, string role)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var userClaims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim(ClaimTypes.Name, $"{user.FirstName} {user.LastName}"),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Role, role)
        };

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: userClaims,
            expires: DateTime.Now.AddDays(1),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    private UserDto MapToUserDto(AppUser user, string token, string role)
    {
        return new UserDto
        {
            Id = user.Id,
            Email = user.Email,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Role = role,
            Token = token
        };
    }
}
