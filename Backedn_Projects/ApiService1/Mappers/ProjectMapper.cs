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
                .ForMember(
                    dest => dest.Title,
                    opt => opt.MapFrom(src => src.ProjectDetailsList.FirstOrDefault() != null
                                              ? src.ProjectDetailsList.FirstOrDefault().Title
                                              : string.Empty)
                )
                .ForMember(
                    dest => dest.Description,
                    opt => opt.MapFrom(src => src.ProjectDetailsList.FirstOrDefault() != null
                                              ? src.ProjectDetailsList.FirstOrDefault().Description
                                              : string.Empty)
                );
        }
    }
}
