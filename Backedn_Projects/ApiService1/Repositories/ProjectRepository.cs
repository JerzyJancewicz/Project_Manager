using ApiService1.Context;
using ApiService1.DTOs;
using ApiService1.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace ApiService1.Repositories
{

    public interface IProjectRepository
    {
        public Task<List<Project>> GetAll(int page, int pageSize);
        public Task Create(Project project, ProjectDetails projectDetails, string email);
        public Task Update(int Id, ProjectDetails projectDetails);
        public Task Delete(int Id);
        public Task<Project?> GetProject(int Id);
        public Task<List<Project>> GetProjects(string email, int page, int pageSize);
        public Task<bool> GetProjectByEmail(string email);
        public Task<Project?> GetProjectContainingDetails(int Id);
        public Task<bool> UserContainsProject(string email, int Id);
        public Task CreateOnUsers(Project project, ProjectDetails projectDetails, string email, List<User> users);
        public Task<List<Project>> GetSharedProjects(string email, int page, int pageSize);
        public Task<List<User>> GetUsersByProjectId(int Id);
    }
    public class ProjectRepository : IProjectRepository
    {
        private readonly IDbContextFactory<ApiServiceDbContext> _context;

        public ProjectRepository(IDbContextFactory<ApiServiceDbContext> context)
        {
            _context = context;
        }

        public async Task<List<Project>> GetAll(int page, int pageSize)
        {
            using (var context = _context.CreateDbContext())
            {
                var projects = await context.Project.ToListAsync();
                var projectDetails = await context.ProjectDetails.ToListAsync();
                foreach (var project in projects)
                {
                    project.LastModified.ToString("yyyy-MM-dd : HH:mm:ss");
                    project.CreatedAt.ToString("yyyy-MM-dd : HH:mm:ss");
                    var currentProjectDetails = projectDetails.FirstOrDefault(src => src.IdProjectDetails == project.ProjectDetailsIdProjectDetails);
                    if (currentProjectDetails != null)
                    {
                        project.IdProjectDetailsNavigation = currentProjectDetails;
                    }
                }
                var paginatedProjects = projects.Skip((page - 1) * pageSize).Take(pageSize).ToList();
                return paginatedProjects;
            }
        }
        public async Task Create(Project project, ProjectDetails projectDetails, string email)
        {
            using (var context = _context.CreateDbContext())
            {
                context.ProjectDetails.Add(projectDetails);
                await context.SaveChangesAsync();

                project.SetCreatedAt();
                project.ProjectDetailsIdProjectDetails = projectDetails.IdProjectDetails;
                
                context.Project.Add(project);
                await context.SaveChangesAsync();

                var user = await context.User.FirstOrDefaultAsync(e => e.Email == email);
                if (user is not null)
                {
                    context.UserProject.Add(new UserProject()
                    {
                        UserId = user.Id,
                        ProjectId = project.IdProject
                    });
                }
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
                    project.LastModified = DateTime.UtcNow;
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
                    var projectDetails = await context.ProjectDetails.FirstOrDefaultAsync(e => e.IdProjectDetails == project.ProjectDetailsIdProjectDetails);
                    if (projectDetails != null)
                    {
                        context.ProjectDetails.Remove(projectDetails);
                    }
                    context.Project.Remove(project);
                    await context.SaveChangesAsync();
                }
            }
        }

        public async Task<Project?> GetProject(int Id)
        {
            using (var context = _context.CreateDbContext())
            {
                var project = await context.Project.FirstOrDefaultAsync(e => e.IdProject == Id);
                if (project != null)
                {
                    project.LastModified.ToString("yyyy-MM-dd : HH:mm:ss");
                    project.CreatedAt.ToString("yyyy-MM-dd : HH:mm:ss");
                }
                return project;
            }
        }

        public async Task<List<Project>> GetProjects(string email, int page, int pageSize)
        {
            using (var context = _context.CreateDbContext())
            {
                var user = await context.User.FirstOrDefaultAsync(e => e.Email == email);
                if (user == null)
                {
                    return new List<Project>();
                }

                var userProjectIds = await context.UserProject
                                                  .Where(up => up.UserId == user.Id)
                                                  .Select(up => up.ProjectId)
                                                  .ToListAsync();

                var exclusiveProjects = new List<Project>();

                foreach (var projectId in userProjectIds)
                {
                    var userCount = await context.UserProject.CountAsync(up => up.ProjectId == projectId);

                    if (userCount == 1)
                    {
                        var project = await context.Project.FirstOrDefaultAsync(p => p.IdProject == projectId);
                        if (project != null)
                        {
                            exclusiveProjects.Add(project);
                        }
                    }
                }
                foreach (var project in exclusiveProjects)
                {
                    project.LastModified.ToString("yyyy-MM-dd : HH:mm:ss");
                    project.CreatedAt.ToString("yyyy-MM-dd : HH:mm:ss");
                    var currentProjectDetails = await context.ProjectDetails.FirstOrDefaultAsync(pd => pd.IdProjectDetails == project.ProjectDetailsIdProjectDetails);
                    if (currentProjectDetails != null)
                    {
                        project.IdProjectDetailsNavigation = currentProjectDetails;
                    }
                }
                var paginatedProjects = exclusiveProjects.Skip((page - 1) * pageSize).Take(pageSize).ToList();
                return paginatedProjects;
            }
        }
        public async Task<List<Project>> GetSharedProjects(string email, int page, int pageSize)
        {
            using (var context = _context.CreateDbContext())
            {
                var user = await context.User.FirstOrDefaultAsync(e => e.Email == email);
                if (user == null)
                {
                    return new List<Project>();
                }

                var userProjectIds = await context.UserProject
                                                  .Where(up => up.UserId == user.Id)
                                                  .Select(up => up.ProjectId)
                                                  .ToListAsync();

                var sharedProjects = new List<Project>();

                foreach (var projectId in userProjectIds)
                {
                    var userCount = await context.UserProject
                                                 .CountAsync(up => up.ProjectId == projectId);

                    if (userCount > 1)
                    {
                        var project = await context.Project
                                                   .FirstOrDefaultAsync(p => p.IdProject == projectId);
                        if (project != null)
                        {
                            sharedProjects.Add(project);
                        }
                    }
                }
                foreach (var project in sharedProjects)
                {
                    project.LastModified.ToString("yyyy-MM-dd : HH:mm:ss");
                    project.CreatedAt.ToString("yyyy-MM-dd : HH:mm:ss");
                    var currentProjectDetails = await context.ProjectDetails
                                                             .FirstOrDefaultAsync(pd => pd.IdProjectDetails == project.ProjectDetailsIdProjectDetails);
                    if (currentProjectDetails != null)
                    {
                        project.IdProjectDetailsNavigation = currentProjectDetails;
                    }
                }
                var paginatedProjects = sharedProjects.Skip((page - 1) * pageSize).Take(pageSize).ToList();
                return paginatedProjects;
            }
        }


        public async Task<bool> GetProjectByEmail(string email)
        {
            using (var context = _context.CreateDbContext())
            {
                var user = await context.User.FirstOrDefaultAsync(e => e.Email == email);
                if (user is not null) 
                {
                    var isProject = await context.UserProject.AnyAsync(e => e.UserId == user.Id);
                    return isProject;
                }
                return false;
            }
        }

        public async Task<Project?> GetProjectContainingDetails(int Id)
        {

            using (var context = _context.CreateDbContext())
            {
                var project = await context.Project.FirstOrDefaultAsync(e => e.IdProject == Id);
                
                if (project is not null)
                {
                    var currentProjectDetails = await context.ProjectDetails.FirstOrDefaultAsync(src => src.IdProjectDetails == project.ProjectDetailsIdProjectDetails);
                    if (currentProjectDetails != null)
                    {
                        project.IdProjectDetailsNavigation = currentProjectDetails;
                    }
                    project.CreatedAt.ToString("yyyy-MM-dd : HH:mm:ss");
                    project.LastModified.ToString("yyyy-MM-dd : HH:mm:ss");
                }
                return project;
            }
        }

        public async Task<bool> UserContainsProject(string email, int id)
        {
            using (var context = _context.CreateDbContext())
            {
                var user = await context.User.FirstOrDefaultAsync(e => e.Email == email);
                if (user is not null)
                {
                    var userProjects = await context.UserProject.Where(e => e.UserId == user.Id).ToListAsync();
                    var isUserProject = userProjects.Any(e => e.ProjectId == id);
                    return isUserProject;
                }
                return false;
            }
        }

        public async Task CreateOnUsers(Project project, ProjectDetails projectDetails, string email, List<User> users)
        {
            using (var context = _context.CreateDbContext())
            {
                context.ProjectDetails.Add(projectDetails);
                await context.SaveChangesAsync();

                project.SetCreatedAt();
                project.ProjectDetailsIdProjectDetails = projectDetails.IdProjectDetails;

                context.Project.Add(project);
                await context.SaveChangesAsync();

                var emailUser = await context.User.FirstOrDefaultAsync(e => e.Email == email);
                if (emailUser is not null)
                {
                    context.UserProject.Add(new UserProject()
                    {
                        UserId = emailUser.Id,
                        ProjectId = project.IdProject
                    });
                }

                List<User?> usersWithId = new List<User?>();
                var doesUserContainsEmailUser = users.Any(e => e.Email == email);
                foreach (var user in users)
                {
                    usersWithId.Add(await context.User.FirstOrDefaultAsync(e => e.Email == user.Email));
                }
                if (!doesUserContainsEmailUser)
                {
                    foreach (var user in usersWithId)
                    {
                        context.UserProject.Add(new UserProject()
                        {
                            UserId = user.Id,
                            ProjectId = project.IdProject
                        });
                    }
                }

                await context.SaveChangesAsync();
            }
        }

        public async Task<List<User>> GetUsersByProjectId(int projectId)
        {
            using (var context = _context.CreateDbContext())
            {
                var userIds = await context.UserProject.Where(e => e.ProjectId == projectId).Select(us => us.UserId).Distinct().ToListAsync();
                var users = await context.User
                                 .Where(u => userIds.Contains(u.Id))
                                 .ToListAsync();
                return users;
            }
        }

    }
}