using ApiService1.DTOs.UserDtos;
using ApiService1.Services;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;

namespace ApiService1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        private string GetUsersEmail(string key)
        {
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(key) as JwtSecurityToken;
            var emailClaim = jsonToken?.Claims.FirstOrDefault(claim => claim.Type == "email");
            return emailClaim?.Value;
        }

        [HttpGet("{token}")]
        public async Task<IActionResult> GetUsers(string token)
        {
            var email = GetUsersEmail(token);
            if (!await _userService.UserExists(email))
            {
                return NotFound();
            }
            return Ok(await _userService.GetUser(email));
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(UserCreate userCreate)
        {
            if (await _userService.UserExistsByEmail(userCreate.Email))
            {
                return Conflict();
            }
            if (!await _userService.CreateUser(userCreate))
            {
                return Conflict();
            }
            return Created("","");
        }
    }
}
