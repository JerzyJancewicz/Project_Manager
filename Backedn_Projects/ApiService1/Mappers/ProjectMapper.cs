using ApiService1.DTOs;
using ApiService1.Entities;
using AutoMapper;

namespace ApiService1.Mappers
{
    public class ProjectMapper : Profile
    {
        public ProjectMapper()
        {
            CreateMap<Project, ProjectGET>()
                .ForMember(src => src.Title, opt => opt.MapFrom(src => src.IdProjectDetailsNavigation.Title))
                .ForMember(src => src.Description, opt => opt.MapFrom(src => src.IdProjectDetailsNavigation.Description))
                .ForMember(src => src.CreateAt, opt => opt.MapFrom(src => src.CreatedAt));

            CreateMap<ProjectCreate, Project>();
            CreateMap<ProjectUpdate, ProjectDetails>();
        }
    }
}
