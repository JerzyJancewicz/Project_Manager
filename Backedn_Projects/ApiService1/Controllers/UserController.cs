using ApiService1.DTOs.UserDtos;
using ApiService1.Services;
using Microsoft.AspNetCore.Mvc;

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

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            return Ok(await _userService.GetUsers());
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetUsers(string Id)
        {
            if (!await _userService.UserExists(Id))
            {
                return NotFound();
            }
            return Ok(await _userService.GetUser(Id));
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
