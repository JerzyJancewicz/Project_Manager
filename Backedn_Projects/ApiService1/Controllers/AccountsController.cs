using ApiService1.Context;
using ApiService1.DTOs.UserDtos;
using ApiService1.Entities;
using ApiService1.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace ApiService1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IAuthorizeService _service;
        private readonly IUserService _userService;

        public AccountsController(IConfiguration config, IAuthorizeService service, IUserService userService)
        {
            _service = service;
            _userService = userService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLogin dto)
        {
            var user = await _userService.UserExistsByEmail(dto.Email);
            if (!user || !await _service.UserPasswordMatches(dto))
                return Unauthorized("Wrong username or password");

            return Ok(new UserLoginResponse
            {
                Token = _service.RefreshToken(dto.Email),
                RefreshToken = _service.RefreshRefToken(dto.Email)
            });
        }

/*        [HttpPost("refresh")]
        public IActionResult RefreshToken(RefreshTokenDto dto)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                tokenHandler.ValidateToken(dto.RefreshToken, new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = _config["JWT:RefIssuer"],
                    ValidAudience = _config["JWT:RefAudience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:RefKey"]!))
                }, out SecurityToken validatedToken);

                var accessToken = _service.RefreshToken();
                var refreshToken = _service.RefreshRefToken();

                return Ok(new RefreshResponseDto
                {
                    Token = accessToken,
                    RefreshToken = refreshToken
                });
            }
            catch
            {
                return Unauthorized();
            }
        }*/

    }
}
