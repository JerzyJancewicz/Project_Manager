using ApiService1.DTOs;
using ApiService1.Repositories;
using AutoMapper;

namespace ApiService1.Services
{
    public interface IUserService
    {
        Task<List<UserGET>> GetUsers();
        Task<UserGET> GetUser(string Id);
        Task<bool> UserExists(string Id);
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
    }
}
