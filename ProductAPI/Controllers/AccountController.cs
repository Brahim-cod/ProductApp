﻿using Microsoft.AspNetCore.Mvc;
using Shered.ModelsDto;
using Shered.Services;

namespace ProductAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly IUserService _userService;

    public AccountController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterDto userDto)
    {
        try
        {
            var registeredUser = await _userService.Register(userDto);
            return Ok(registeredUser);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginDto loginDto)
    {
        try
        {
            var loguser = await _userService.Login(loginDto);
            return Ok(loguser);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
