using ApiService1.DTOs;
using ApiService1.Services;
using Microsoft.AspNetCore.Mvc;

namespace ApiService1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectService _service;

        public ProjectController(IProjectService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetProject()
        {
            return Ok(await _service.GetAllProjects());
        }

        [HttpPost]
        public async Task<IActionResult> CreateProject(ProjectCreate project)
        {
            await _service.CreateProject(project);
            return Created("", "");
        }

        [HttpPut("{Id}")]
        public async Task<IActionResult> UpdateProject(int Id, ProjectUpdate project)
        {
            await _service.UpdateProject(Id, project);
            return Ok();
        }
    }
}
