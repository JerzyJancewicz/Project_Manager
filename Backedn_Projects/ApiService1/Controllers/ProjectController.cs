using ApiService1.DTOs;
using ApiService1.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace ApiService1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectService _service;
        private readonly IAuthorizeService _authorizeService;

        public ProjectController(IProjectService service, IAuthorizeService authorizeService)
        {
            _service = service;
            _authorizeService = authorizeService;
        }

        private string GetUsersEmail(string key)
        {
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(key) as JwtSecurityToken;
            var emailClaim = jsonToken?.Claims.FirstOrDefault(claim => claim.Type == "email");
            return emailClaim?.Value;
        }

        [HttpGet("{key}")]
        public async Task<IActionResult> GetProject(string key)
        {
            try
            {
                _authorizeService.AuthorizeToken(key);
            }
            catch
            {
                return Unauthorized();
            }
            var userEmail = GetUsersEmail(key);
            return Ok(await _service.GetProjectsByEmail(userEmail));
        }

        [HttpPost("{key}")]
        public async Task<IActionResult> CreateProject(ProjectCreate project, string key)
        {
            try
            {
                _authorizeService.AuthorizeToken(key);
            }
            catch
            {
                return Unauthorized();
            }
            var userEmail = GetUsersEmail(key);
            Console.WriteLine("cosocssco" + userEmail); 
            await _service.CreateProject(project, userEmail);
            return Created("", "");
        }

        [HttpPut("{Id}/{key}")]
        public async Task<IActionResult> UpdateProject(int Id, string key, ProjectUpdate project)
        {
            try
            {
                _authorizeService.AuthorizeToken(key);
            }
            catch
            {
                return Unauthorized();
            }
            if (!await _service.ProjectExists(Id))
            {
                return NotFound();
            }
            await _service.UpdateProject(Id, project);
            return Ok();
        }

        [HttpDelete("{Id}/{key}")]
        public async Task<IActionResult> DeleteProject(int Id, string key)
        {
            try
            {
                _authorizeService.AuthorizeToken(key);
            }
            catch
            {
                return Unauthorized();
            }
            if (!await _service.ProjectExists(Id))
            {
                return NotFound();
            }
            await _service.DeleteProject(Id);
            return Ok();
        }
    }
}
