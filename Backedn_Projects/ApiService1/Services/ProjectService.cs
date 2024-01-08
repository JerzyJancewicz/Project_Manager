using ApiService1.DTOs;
using ApiService1.Entities;
using ApiService1.Repositories;
using AutoMapper;

namespace ApiService1.Services
{

    public interface IProjectService
    {
        public Task<List<ProjectGET>> GetAllProjects();
        public Task CreateProject(ProjectCreate projectCreate);
        public Task UpdateProject(int Id, ProjectUpdate projectUpdate);
        public Task DeleteProject(int Id);
        public Task<bool> ProjectExists(int Id);
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

        public async Task CreateProject(ProjectCreate projectCreate)
        {
            var project = _mapper.Map<Project>(projectCreate);

            var projectDetails = new ProjectDetails()
            {
                Title = projectCreate.Title,
                Description = projectCreate.Description
            };

            await _repository.Create(project, projectDetails);
        }

        public async Task UpdateProject(int Id, ProjectUpdate projectUpdate)
        {
            var projectDetails = _mapper.Map<ProjectDetails>(projectUpdate);
            var project = _repository.GetProjectById(Id);
            if (project != null)
            {
                await _repository.Update(Id, projectDetails);
            }
        }

        public async Task DeleteProject(int Id)
        {
            var project = _repository.GetProjectById(Id);
            if (project != null)
            {
                await _repository.Delete(Id);
            }
        }

        public async Task<bool> ProjectExists(int Id)
        {
            var project = await _repository.GetProjectById(Id);
            return project != null;
        }
    }
}
