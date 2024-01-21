using ApiService1.DTOs;
using ApiService1.DTOs.UserDtos;
using ApiService1.Entities;
using ApiService1.Repositories;
using AutoMapper;

namespace ApiService1.Services
{
    public interface IUserService
    {
        public Task<List<UserGET>> GetUsers();
        public Task<UserGET> GetUser(string Id);
        public Task<bool> UserExists(string Id);
        public Task<bool> CreateUser(UserCreate userCreate);
        public Task<bool> UserExistsByEmail(string email);
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

        public async Task<UserGET> GetUser(string Id)
        {
            var user = await _repository.GetUserById(Id);
            var userDto = _mapper.Map<UserGET>(user);

            return userDto;
        }

        public async Task<List<UserGET>> GetUsers()
        {
            var users = await _repository.GetAllUsers();
            var usersDto = _mapper.Map<List<UserGET>>(users);

            return usersDto;
        }

        public async Task<bool> UserExists(string Id)
        {
            return await _repository.UserExistsById(Id);
        }

        public async Task<bool> UserExistsByEmail(string email)
        {
            return await _repository.UserExistsByUsername(email);
        }
    }
}
