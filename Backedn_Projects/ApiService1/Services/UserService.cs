using ApiService1.DTOs;
using ApiService1.DTOs.RoleDtos;
using ApiService1.DTOs.UserDtos;
using ApiService1.Entities;
using ApiService1.Repositories;
using AutoMapper;

namespace ApiService1.Services
{
    public interface IUserService
    {
        public Task<UserGET> GetUser(string email);
        public Task<bool> UserExists(string email);
        public Task<bool> CreateUser(UserCreate userCreate);
        public Task<bool> UserExistsByEmail(string email);
        public Task<RoleGET> GetRoleByEmail(string email);
    }

    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<bool> CreateUser(UserCreate userCreate)
        {
            var user = _mapper.Map<User>(userCreate);
            var userDetails = _mapper.Map<UserDetails>(userCreate);
            var password = userCreate.Password;
            return await _repository.Create(user, password, userDetails);
        }

        public async Task<UserGET> GetUser(string email)
        {
            var user = await _repository.GetUserById(email);
            var userDto = _mapper.Map<UserGET>(user);

            return userDto;
        }

        public async Task<bool> UserExists(string email)
        {
            return await _repository.UserExistsById(email);
        }

        public async Task<bool> UserExistsByEmail(string email)
        {
            return await _repository.UserExistsByUsername(email);
        }
        public async Task<RoleGET> GetRoleByEmail(string email)
        {
            var role = await _repository.GetRole(email);
            var roleDto = _mapper.Map<RoleGET>(role);

            if(roleDto is null)
            {
                roleDto = new RoleGET()
                {
                    Name = "guest",
                };
            }
            return (roleDto);
        }

    }
}
