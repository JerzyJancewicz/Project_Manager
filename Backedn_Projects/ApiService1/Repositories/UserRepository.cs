using ApiService1.Context;
using ApiService1.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ApiService1.Repositories
{
    public interface IUserRepository
    {
        public Task<User?> GetUserById(string email);
        public Task<bool> UserExistsById(string email);
        public Task<bool> Create(User user, string password, UserDetails userDetails);
        public Task<bool> UserExistsByUsername(string email);
        public Task<Role?> GetRole(string email);


    }
    public class UserRepository : IUserRepository
    {
        private readonly IDbContextFactory<ApiServiceDbContext> _context;
        private readonly UserManager<User> _userManager;

        public UserRepository(IDbContextFactory<ApiServiceDbContext> contextFactory, UserManager<User> userManager)
        {
            _context = contextFactory;
            _userManager = userManager;
        }

        public async Task<bool> Create(User user, string password, UserDetails userDetails)
        {
            IdentityResult result;
            using (var context = _context.CreateDbContext())
            {
                context.UserDetails.Add(userDetails);
                await context.SaveChangesAsync();

                user.UserIdUser = userDetails.IdUserDetails;
                user.UserName = user.Email;

                result = await _userManager.CreateAsync(user, password);
                if (!result.Succeeded)
                {
                    context.UserDetails.Remove(userDetails);
                    await context.SaveChangesAsync();
                    return false;
                }

                await _userManager.AddToRoleAsync(user, "user");
                return result.Succeeded;
            }
        }

        public async Task<User?> GetUserById(string email)
        {
            using (var contex = _context.CreateDbContext())
            {
                var user = await contex.User.FirstOrDefaultAsync(e => e.Email == email);
                if (user is not null)
                {
                    var userDetails = await contex.UserDetails.FirstOrDefaultAsync(e => e.IdUserDetails == user.UserIdUser);
                    if (userDetails is not null)
                    {
                        user.IdUserDetailsNavigation = userDetails;
                    }
                }
                return user;
            }
        }

        public async Task<bool> UserExistsByUsername(string email)
        {
            using (var context = _context.CreateDbContext())
            {
                var user = await context.User.FirstOrDefaultAsync(e => e.Email == email);
                return user is not null;
            }
        }

        public async Task<bool> UserExistsById(string email)
        {
            using (var contex = _context.CreateDbContext())
            {
                var user = await contex.User.FirstOrDefaultAsync(e => e.Email == email);
                return user is not null;
            }
        }

        public async Task<Role?> GetRole(string email)
        {
            using (var contex = _context.CreateDbContext())
            {
                var user = await contex.User.FirstOrDefaultAsync(e => e.Email == email);

                if (user is not null)
                {
                    var userRole = await contex.UserRoles.FirstOrDefaultAsync(e => e.UserId == user.Id);
                    if (userRole is not null)
                    {
                        var role = await contex.Role.FirstOrDefaultAsync(e => e.Id == userRole.RoleId);
                        return role;
                    }
                }
                return null;
            }
        }
    }
}
