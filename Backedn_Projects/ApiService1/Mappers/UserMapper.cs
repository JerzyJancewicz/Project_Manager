using ApiService1.DTOs;
using ApiService1.DTOs.UserDtos;
using ApiService1.Entities;
using AutoMapper;

namespace ApiService1.Mappers
{
    public class UserMapper : Profile
    {
        public UserMapper()
        {
            CreateMap<User, UserGET>()
                .ForMember(src => src.Name, opt => opt.MapFrom(src => src.IdUserDetailsNavigation.Name))
                .ForMember(src => src.Surname, opt => opt.MapFrom(src => src.IdUserDetailsNavigation.Surname));
            CreateMap<UserGET, User>();

            CreateMap<UserCreate, User>();
            CreateMap<UserCreate, UserDetails>();
        }
    }
}
