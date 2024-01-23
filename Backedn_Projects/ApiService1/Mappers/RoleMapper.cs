using ApiService1.DTOs.RoleDtos;
using ApiService1.Entities;
using AutoMapper;

namespace ApiService1.Mappers
{
    public class RoleMapper : Profile
    {
        public RoleMapper()
        {
            CreateMap<RoleGET, Role>();
            CreateMap<Role, RoleGET>();
        }
    }
}
