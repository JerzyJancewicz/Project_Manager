using ApiService1.DTOs.UserDtos;
using ApiService1.Entities;
using Microsoft.AspNetCore.Identity;

namespace ApiService1.Repositories
{
    public interface IAuthorizeRepository
    {
        public Task<bool> MatchPassword(UserLogin userLogin);
    }
    public class AuthorizeRepository : IAuthorizeRepository
    {
        private readonly UserManager<User> _userManager;

        public AuthorizeRepository(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<bool> MatchPassword(UserLogin userLogin)
        {
            var userManager = await _userManager.FindByEmailAsync(userLogin.Email);
            if (userManager is not null && userManager.PasswordHash is not null)
            {
                var result = _userManager.PasswordHasher.VerifyHashedPassword(userManager, userManager.PasswordHash, userLogin.Password);
                return result == PasswordVerificationResult.Success;
            }
            return false;
        }
    }
}
