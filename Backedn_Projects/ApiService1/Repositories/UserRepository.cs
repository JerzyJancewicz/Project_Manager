using ApiService1.Context;
using ApiService1.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ApiService1.Repositories
{
    public interface IUserRepository
    {
        Task<List<User>> GetAllUsers();
        Task<User?> GetUserById(string id);
        Task<bool> UserExistsById(string id);
        Task<bool> Create(User user, string password, UserDetails userDetails);
        Task<bool> UserExistsByUsername(string email);
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
                }
                return result.Succeeded;
            }
        }

        public async Task<List<User>> GetAllUsers()
        {
            using (var contex = _context.CreateDbContext())
            {
                var users = await contex.User.ToListAsync();
                foreach (var user in users)
                {
                    var userDetails = await contex.UserDetails.FirstOrDefaultAsync(e => e.IdUserDetails == user.UserIdUser);
                    if (userDetails is not null)
                    {
                        user.IdUserDetailsNavigation = userDetails;
                    }
                }
                return users;
            }
        }

        public async Task<User?> GetUserById(string id)
        {
            using (var contex = _context.CreateDbContext())
            {
                var user = await contex.User.FirstOrDefaultAsync(e => e.Id == id);
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

        public async Task<bool> UserExistsById(string id)
        {
            using (var contex = _context.CreateDbContext())
            {
                var user = await contex.User.FirstOrDefaultAsync(e => e.Id == id);
                return user is not null;
            }
        }

    }
}
