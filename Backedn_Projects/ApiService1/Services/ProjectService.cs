using ApiService1.DTOs;
using ApiService1.Entities;
using ApiService1.Repositories;
using AutoMapper;

namespace ApiService1.Services
{

    public interface IProjectService
    {
        public Task<List<ProjectGET>> GetAllProjects();
        public Task CreateProject(ProjectCreate projectCreate, string email);
        public Task UpdateProject(int Id, ProjectUpdate projectUpdate);
        public Task DeleteProject(int Id);
        public Task<bool> ProjectExists(int Id);
        public Task<List<ProjectGET>> GetProjectsByEmail(string email);
        public Task<bool> ProjectExistsByEmail(string email);
        public Task<ProjectGET> GetProjectsById(int id);
        public Task<bool> UserContainsProjectById(string email, int Id);
        public Task<ProjectGET> GetProjectContainingDetailsById(int Id);
    }

    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository _repository;
        private readonly IMapper _mapper;

        public ProjectService(IProjectRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<ProjectGET>> GetAllProjects()
        {
            var projects = await _repository.GetAll();
            var dtos = _mapper.Map<List<ProjectGET>>(projects);
            return dtos;
        }

        public async Task CreateProject(ProjectCreate projectCreate, string email)
        {
            var project = _mapper.Map<Project>(projectCreate);

            var projectDetails = new ProjectDetails()
            {
                Title = projectCreate.Title,
                Description = projectCreate.Description
            };

            await _repository.Create(project, projectDetails, email);
        }

        public async Task UpdateProject(int Id, ProjectUpdate projectUpdate)
        {
            var projectDetails = _mapper.Map<ProjectDetails>(projectUpdate);
            var project = _repository.GetProject(Id);
            if (project is not null)
            {
                await _repository.Update(Id, projectDetails);
            }
        }

        public async Task DeleteProject(int Id)
        {
            var project = _repository.GetProject(Id);
            if (project is not null)
            {
                await _repository.Delete(Id);
            }
        }

        public async Task<bool> ProjectExists(int Id)
        {
            var project = await _repository.GetProject(Id);
            return project is not null;
        }

        public async Task<List<ProjectGET>> GetProjectsByEmail(string email)
        {
            var projects = await _repository.GetProjects(email);
            var projectsDto = _mapper.Map<List<ProjectGET>>(projects);

            return projectsDto;

        }

        public async Task<bool> ProjectExistsByEmail(string email)
        {
            return await _repository.GetProjectByEmail(email);
        }

        public async Task<ProjectGET> GetProjectsById(int id)
        {
            var project = await _repository.GetProject(id);
            var projectDto = _mapper.Map<ProjectGET>(project);

            return projectDto;
        }

        public async Task<bool> UserContainsProjectById(string email, int Id)
        {
            return await _repository.UserContainsProject(email, Id);
        }

        public async Task<ProjectGET> GetProjectContainingDetailsById(int Id)
        {
            var project = await _repository.GetProjectContainingDetails(Id);
            var projectDto = _mapper.Map<ProjectGET>(project);

            return projectDto;
        }
    }
}
