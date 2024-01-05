using ApiService1.DTOs;
using ApiService1.Entities;
using ApiService1.Repositories;
using AutoMapper;

namespace ApiService1.Services
{

    public interface IProjectService
    {
        public Task<List<ProjectGET>> GetAllProjects();    }

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
    }
}
