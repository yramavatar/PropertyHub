using AutoMapper;
using DATA_ACCESS_LAYER.DTOs;
using DATA_ACCESS_LAYER.Models;

namespace PropertyHubWebApi.MappingProfiles
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile() 
        {
            CreateMap<UserDTO, User>()
              .ForMember(dest => dest.userType, opt => opt.MapFrom(src => src.userType.ToString()))
              .ReverseMap();
        }
    }
}
