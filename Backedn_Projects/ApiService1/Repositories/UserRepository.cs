using ApiService1.Context;
using ApiService1.Entities;
using Microsoft.EntityFrameworkCore;

namespace ApiService1.Repositories
{
    public interface IUserRepository
    {
        Task<List<User>> GetAllUsers();
        Task<User?> GetUserById(string id);
        Task<bool> UserExistsById(string id);
    }
    public class UserRepository : IUserRepository
    {
        private readonly IDbContextFactory<ApiServiceDbContext> _context;

        public UserRepository(IDbContextFactory<ApiServiceDbContext> contextFactory)
        {
            _context = contextFactory;
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

        public async Task<bool> UserExistsById(string id)
        {
            using (var contex = _context.CreateDbContext())
            {
                var user = await contex.User.FirstOrDefaultAsync(e => e.Id == id);
                return user != null;
            }
        }
    }
}
