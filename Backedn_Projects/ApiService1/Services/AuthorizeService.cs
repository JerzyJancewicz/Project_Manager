using ApiService1.DTOs.UserDtos;
using ApiService1.Repositories;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ApiService1.Services
{
    public interface IAuthorizeService
    {
        public SecurityToken AuthorizeToken(string key);
        public string RefreshToken(string userEmail);
        public string RefreshRefToken(string userEmail);
        public Task<bool> UserPasswordMatches(UserLogin userLogin);
    }
    public class AuthorizeService : IAuthorizeService
    {
        private IConfiguration _config;
        private readonly IAuthorizeRepository _repository;

        public AuthorizeService(IConfiguration config, IAuthorizeRepository repository)
        {
            _config = config;
            _repository = repository;
        }

        public SecurityToken AuthorizeToken(string key)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            tokenHandler.ValidateToken(key, new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = _config["JWT:Issuer"],
                ValidAudience = _config["JWT:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:Key"]!))
            }, out SecurityToken validatedToken);
            return validatedToken;
        }

        public string RefreshToken(string userEmail)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescription = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Email, userEmail)
                }),
                Issuer = _config["JWT:Issuer"],
                Audience = _config["JWT:Audience"],
                Expires = DateTime.UtcNow.AddMinutes(45),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:Key"]!)),
                    SecurityAlgorithms.HmacSha256
                )
            };
            var token = tokenHandler.CreateToken(tokenDescription);
            var stringifiedToken = tokenHandler.WriteToken(token);
            return stringifiedToken;
        }

        public string RefreshRefToken(string userEmail)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var refTokenDescription = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Email, userEmail)
                }),
                Issuer = _config["JWT:RefIssuer"],
                Audience = _config["JWT:RefAudience"],
                Expires = DateTime.UtcNow.AddDays(3),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:RefKey"]!)),
                    SecurityAlgorithms.HmacSha256
                )
            };
            var refToken = tokenHandler.CreateToken(refTokenDescription);
            var stringifiedRefToken = tokenHandler.WriteToken(refToken);
            return stringifiedRefToken;
        }

        public async Task<bool> UserPasswordMatches(UserLogin userLogin)
        {
            return await _repository.MatchPassword(userLogin);
        }
    }
}
