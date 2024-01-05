using ApiService1.Context;
using ApiService1.DTOs;
using ApiService1.Entities;
using Microsoft.EntityFrameworkCore;

namespace ApiService1.Repositories
{

    public interface IProjectRepository
    {
        public Task<List<Project>> GetAll();
        public Task Create(Project project, ProjectDetails projectDetails);
        public Task Update(int Id, ProjectDetails projectDetails);
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
            foreach (var project in projects)
            {
                var currentProjectDetails = projectDetails.FirstOrDefault(src => src.IdProjectDetails == project.ProjectDetailsIdProjectDetails);
                if (currentProjectDetails != null)
                {
                    project.IdProjectDetailsNavigation = currentProjectDetails;
                }
            }
            return projects;
        }
        public async Task Create(Project project, ProjectDetails projectDetails)
        {

            _context.ProjectDetails.Add(projectDetails);
            await _context.SaveChangesAsync();

            var projectIdDetails = _context.ProjectDetails.Max(e => e.IdProjectDetails);

            project.SetCreatedAt();
            project.ProjectDetailsIdProjectDetails = projectIdDetails;

            _context.Project.Add(project);
            await _context.SaveChangesAsync();
        }

        public async Task Update(int Id, ProjectDetails projectDetails)
        {
            var project = await _context.Project.FirstOrDefaultAsync(e => e.IdProject == Id);
            if (project != null)
            {
                projectDetails.IdProjectDetails = project.ProjectDetailsIdProjectDetails;
                _context.ProjectDetails.Update(projectDetails);
                await _context.SaveChangesAsync();
            }
        }
    }
}