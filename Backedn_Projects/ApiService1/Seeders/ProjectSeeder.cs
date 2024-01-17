using ApiService1.Context;
using ApiService1.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace ApiService1.Seeders
{
    public class ProjectSeeder
    {
        private readonly ApiServiceDbContext _context;
        private readonly UserManager<User>? _userManager;
        private readonly RoleManager<Role>? _roleManager;
        public ProjectSeeder(ApiServiceDbContext context, UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
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

                var projectDetailsId = await _context.ProjectDetails.FirstOrDefaultAsync(e => e.IdProjectDetails == 1);
                if (projectDetailsId != null)
                {
                    var project = new Project()
                    {
                        CreatedAt = DateTime.UtcNow,
                        LastModified = DateTime.UtcNow,
                        ProjectDetailsIdProjectDetails = projectDetailsId.IdProjectDetails
                    };
                    if (!_context.Project.Any())
                    {
                        _context.Project.Add(project);
                    }
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
                await _context.SaveChangesAsync();

                IdentityResult createUserResult = new IdentityResult();
                var userDetailsId = await _context.UserDetails.FirstOrDefaultAsync(e => e.IdUserDetails == 1);
                if (userDetailsId != null)
                {
                    var user = new User { UserName = "username", Email = "email@example.com", UserIdUser = userDetailsId.IdUserDetails };
                    if (_userManager is not null)
                    {
                        createUserResult = await _userManager.CreateAsync(user, "Password123!");
                    }
                }

                List<IdentityResult> createRoleResults = new List<IdentityResult>();
                if (_roleManager is not null)
                {
                    var roles = new List<Role>() 
                    {
                        new Role(){ Name = "guest"},
                        new Role(){ Name = "user"},
                        new Role(){ Name = "admin"}
                    };
                    foreach (var role in roles)
                    {
                        createRoleResults.Add(await _roleManager.CreateAsync(role));
                    }
                }

                var userId = await _context.User.FirstOrDefaultAsync(e => e.UserIdUser == 1);
                if (_userManager is not null && createUserResult.Succeeded && createRoleResults.Where(e => e.Succeeded).Count() == 3 && userId is not null)
                {
                    IdentityResult addToRoleResult = await _userManager.AddToRoleAsync(userId, "admin");
                }

                if (!_context.UserProject.Any())
                {
                    var dbUser = _context.User.FirstOrDefault(e => e.UserIdUser == 1);
                    var dbProject = _context.Project.FirstOrDefault(e => e.ProjectDetailsIdProjectDetails == 1);
                    if (dbUser is not null && dbProject is not null)
                    {
                        var userProject = new UserProject()
                        {
                            UserId = dbUser.Id,
                            ProjectId = dbProject.IdProject
                        };
                        _context.UserProject.Add(userProject);
                    }
                }
                await _context.SaveChangesAsync();
            }
        }
    }
}
