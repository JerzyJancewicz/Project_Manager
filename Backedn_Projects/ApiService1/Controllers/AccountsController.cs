using ApiService1.Context;
using ApiService1.DTOs.RoleDtos;
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

            var userRole = await _userService.GetRoleByEmail(dto.Email);

            return Ok(new UserLoginResponse
            {
                Token = _service.RefreshToken(dto.Email, userRole.Name),
                RefreshToken = _service.RefreshRefToken(dto.Email)
            });
        }
    }
}
