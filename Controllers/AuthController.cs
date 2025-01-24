using AutoMapper;
using MeteoLink.Attributes;
using MeteoLink.Data.Models;
using MeteoLink.Dto;
using MeteoLink.Repositories;
using MeteoLink.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Authentication;

namespace MeteoLink.Controllers
{
    [MeteoLinkV1Route]
    [ApiController]
    [Authorize]
    public class AuthController : ControllerBase
    {
        private readonly ITokenService _tokenService;
        private readonly IUserRepository _userService;
        private readonly IMapper mapper;
        private readonly IPasswordHashService _passwordHashService;

        public AuthController(IMapper mapper, IUserRepository _userService, ITokenService tokenService, IPasswordHashService passwordHashService)
        {
            this._userService = _userService;
            this.mapper = mapper;
            this._tokenService = tokenService;
            this._passwordHashService = passwordHashService;
        }


        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            if (ModelState.IsValid)
            {
                var user = await _userService.FindByNameAsync(loginDto.Name);

                if (user == null || !_passwordHashService.VerifyPassword(user, loginDto.Password, user.PasswordHash))
                {
                    return Unauthorized("Invalid username or password");
                }

                var token = _tokenService.CreateToken(user, loginDto.Name);
                return Ok(new { Token = token });
            }

            return BadRequest("Invalid body data");
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            if (ModelState.IsValid)
            {
                var user = await _userService.FindByNameAsync(registerDto.Name);
                if (user != null)
                {
                    return Conflict("User already exists");
                }

                var newUser = new UserModel
                {
                    Name = registerDto.Name,
                    Email = registerDto.Email,
                    PasswordHash = _passwordHashService.HashPassword(new UserModel(), registerDto.Password),
                    CreatedAt = DateTime.UtcNow
                };

                await _userService.CreateAsync(newUser);
                return Ok(new { Message = "User created successfully" });
            }

            return BadRequest("Invalid body data");
        }
    }
}
