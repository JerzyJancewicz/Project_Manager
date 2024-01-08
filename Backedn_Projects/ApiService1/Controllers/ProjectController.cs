using ApiService1.DTOs;
using ApiService1.Services;
using Microsoft.AspNetCore.Mvc;
using System.Web.Http.Cors;

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
            if(!await _service.ProjectExists(Id))
            {
                return NotFound();
            }
            await _service.UpdateProject(Id, project);
            return Ok();
            //Conflict
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteProject(int Id)
        {
            if (!await _service.ProjectExists(Id))
            {
                return NotFound();
            }
            await _service.DeleteProject(Id);
            return Ok();
        }
    }
}
