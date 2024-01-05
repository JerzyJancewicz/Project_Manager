using ApiService1.Context;
using ApiService1.Entities;
using System.Data;

namespace ApiService1.Seeders
{
    public class ProjectSeeder
    {
        private readonly ApiServiceDbContext _context;

        public ProjectSeeder(ApiServiceDbContext context)
        {
            _context = context;
        }

        public async Task Seed()
        {
            if (await _context.Database.CanConnectAsync())
            {
                if (!_context.ProjectDetails.Any())
                {
                    var projectDetails = new ProjectDetails()
                    {
                        Title = "TIN Project",
                        Description = "Web SPA Project which should use web API",
                    };
                    _context.ProjectDetails.Add(projectDetails);
                }
                await _context.SaveChangesAsync();
                if (!_context.Project.Any())
                {
                    var project = new Project()
                    {
                        CreatedAt = DateTime.UtcNow,
                        LastModified = DateTime.UtcNow,
                        ProjectDetailsIdProjectDetails = 1
                    };
                    _context.Project.Add(project);
                }
                if (!_context.UserDetails.Any())
                {
                    var userDetails = new UserDetails()
                    {
                        Name = "Jerzy",
                        Surname = "Jancewicz"
                    };
                    _context.UserDetails.Add(userDetails);
                }
                if (!_context.Role.Any())
                {
                    var roles = new List<Role>()
                    {
                        new Role()
                        {
                            RoleName = "guest"
                        },
                        new Role()
                        {
                            RoleName = "user"
                        },
                        new Role()
                        {
                            RoleName = "admin"
                        }
                    };
                    foreach (var role in roles)
                    {
                        _context.Role.Add(role);
                    }
                }
                await _context.SaveChangesAsync();

                if (!_context.User.Any())
                {
                    var user = new User()
                    {
                        Email = "user@user.com",
                        Password = "user",
                        RoleIdRole = 2,
                        UserIdUser = 1
                    };
                    _context.User.Add(user);
                }
                if (!_context.UserProject.Any())
                {
                    var userProject = new UserProject()
                    {
                        UserId = 1,
                        ProjectId = 1
                    };
                    _context.UserProject.Add(userProject);
                }
                await _context.SaveChangesAsync();
            }
        }
    }
}
