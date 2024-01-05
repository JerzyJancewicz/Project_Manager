using ApiService1.Context;
using ApiService1.Entities;
using Microsoft.EntityFrameworkCore;

namespace ApiService1.Repositories
{

    public interface IProjectRepository
    {
        public Task<List<Project>> GetAll();
    }
    public class ProjectRepository : IProjectRepository
    {
        private readonly ApiServiceDbContext _context;

        public ProjectRepository(ApiServiceDbContext context)
        {
            _context = context;
        }

        public async Task<List<Project>> GetAll()
        {
            var projects = await _context.Project.ToListAsync();
            var projectDetails = await _context.ProjectDetails.ToListAsync();
            projects.ForEach(e => e.IdProjectDetailsNavigation = projectDetails.FirstOrDefault(src => src.IdProjectDetails == e.ProjectDetailsIdProjectDetails));
            return projects;
        }
    }
}
