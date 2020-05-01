using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PWAppApi.Models.Dto;
using PWAppApi.Models.Entity;
using PWAppApi.Services.Auth;

namespace PWAppApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }
        [HttpGet("Token")]
        [AllowAnonymous]
        public async Task<IActionResult> Token(string username, string password)
        {
            var result = await _authService.LoginAsync(username, password);
            if (!result.Succeeded)
            {
                return StatusCode(401, new { error = "Invalid username or password" });
            }
            // crete JWT-токен
            var access_token = await _authService.GetAuthDataAsync(username);
            return Ok(access_token);
        }

        [HttpGet("Logout")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Logout()
        {
            await _authService.LogoutAsync();
            return Ok(new { message = "Exit" });
        }
        
        [HttpGet("Register")]
        [AllowAnonymous]        
        public async Task<IActionResult> Register([FromQuery]UserCreateDto userDto, string password)
        {
            var user = _authService.CreteFromDto(userDto);
            var result = await _authService.SingUpAsync(user, password);
            if (!result.Succeeded)
            {
                return StatusCode(500, new { error = result.Errors.SelectMany(x=>x.Description) } );
            }
            await _authService.CreateBalance(user);
            return Ok(new { message = "Registration complite" });
        }
    }
}