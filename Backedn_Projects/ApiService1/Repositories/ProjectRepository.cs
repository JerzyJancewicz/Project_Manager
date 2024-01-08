using ApiService1.Context;
using ApiService1.DTOs;
using ApiService1.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace ApiService1.Repositories
{

    public interface IProjectRepository
    {
        public Task<List<Project>> GetAll();
        public Task Create(Project project, ProjectDetails projectDetails);
        public Task Update(int Id, ProjectDetails projectDetails);
        public Task Delete(int Id);
        public Task<Project?> GetProjectById(int Id);
    }
    public class ProjectRepository : IProjectRepository
    {
        private readonly IDbContextFactory<ApiServiceDbContext> _context;

        public ProjectRepository(IDbContextFactory<ApiServiceDbContext> context)
        {
            _context = context;
        }

        public async Task<List<Project>> GetAll()
        {
            using (var context = _context.CreateDbContext())
            {
                var projects = await context.Project.ToListAsync();
                var projectDetails = await context.ProjectDetails.ToListAsync();
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
        }
        public async Task Create(Project project, ProjectDetails projectDetails)
        {
            using (var context = _context.CreateDbContext())
            {
                context.ProjectDetails.Add(projectDetails);
                await context.SaveChangesAsync();

                project.SetCreatedAt();
                project.ProjectDetailsIdProjectDetails = projectDetails.IdProjectDetails;

                context.Project.Add(project);
                await context.SaveChangesAsync();
            }
        }

        public async Task Update(int Id, ProjectDetails projectDetails)
        {
            using (var context = _context.CreateDbContext())
            {
                var project = await context.Project.FirstOrDefaultAsync(e => e.IdProject == Id);
                if (project != null)
                {
                    projectDetails.IdProjectDetails = project.ProjectDetailsIdProjectDetails;
                    context.ProjectDetails.Update(projectDetails);
                    await context.SaveChangesAsync();
                }
            }
        }

        public async Task Delete(int Id)
        {
            using (var context = _context.CreateDbContext())
            {
                var project = await context.Project.FirstOrDefaultAsync(e => e.IdProject == Id);
                if (project != null)
                {
                    context.Project.Remove(project);
                    await context.SaveChangesAsync();
                }
            }
        }

        public async Task<Project?> GetProjectById(int Id)
        {
            using (var context = _context.CreateDbContext())
            {
                return await context.Project.FirstOrDefaultAsync(e => e.IdProject == Id);
            }
        }
    }
}