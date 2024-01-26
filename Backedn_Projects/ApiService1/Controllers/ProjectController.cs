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
        private string GetUsersRole(string key)
        {
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(key) as JwtSecurityToken;
            var roleClaim = jsonToken?.Claims.FirstOrDefault(claim => claim.Type == "role");
            return roleClaim?.Value;
        }

        [HttpGet("{key}/group/{isGroup}")]
        public async Task<IActionResult> GetProject(string key, int page, int pageSize, bool isGroup)
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
            var userRole = GetUsersRole(key);
            if (userRole == "admin")
            {
                return Ok(await _service.GetAllProjects(page, pageSize));
            }
            if (isGroup)
            {
                return Ok(await _service.GetSharedProjectsByEmail(userEmail, page, pageSize));
            }
            else
            {
                return Ok(await _service.GetProjectsByEmail(userEmail, page, pageSize));
            }
        }

        [HttpGet("{Id}/{key}")]
        public async Task<IActionResult> GetProjectById(string key, int Id)
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
            var userRole = GetUsersRole(key);
            if (!await _service.UserContainsProjectById(userEmail, Id) && userRole != "admin")
            {
                return Unauthorized();
            }
            return Ok(await _service.GetProjectContainingDetailsById(Id));
        }

        [HttpPost("{key}/{isGroup}")]
        public async Task<IActionResult> CreateProject(ProjectCreate project, string key, bool isGroup)
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
            if (isGroup)
            {
                await _service.CreateProjectOnUsers(project, userEmail, project.users);
                return Created("", "");
            }
            else
            {
                await _service.CreateProject(project, userEmail);
                return Created("", "");
            }
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
            var userEmail = GetUsersEmail(key);
            var userRole = GetUsersRole(key);
            if (!await _service.ProjectExistsByEmail(userEmail) && userRole != "admin")
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
            var userEmail = GetUsersEmail(key);
            var userRole = GetUsersRole(key);
            if (!await _service.ProjectExistsByEmail(userEmail) && userRole != "admin")
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
