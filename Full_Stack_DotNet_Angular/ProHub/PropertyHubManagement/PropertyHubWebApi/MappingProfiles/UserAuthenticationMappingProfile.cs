using AutoMapper;
using DATA_ACCESS_LAYER.DTOs;
using DATA_ACCESS_LAYER.Models;

namespace PropertyHubWebApi.MappingProfiles
{
    public class UserAuthenticationMappingProfile : Profile
    {
        public UserAuthenticationMappingProfile()
        {
             CreateMap<User,UserAuthenticationDTO>().ReverseMap();
        
        }
    }
}
