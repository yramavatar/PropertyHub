using AutoMapper;
using DATA_ACCESS_LAYER.DTOs;
using DATA_ACCESS_LAYER.Models;

namespace PropertyHubWebApi.MappingProfiles
{
    public class PropertyMappingProfile : Profile
    {
         public PropertyMappingProfile() 
        {

            CreateMap<PropertyDTO, Property>()
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.PropertyType))
                .ForMember(dest => dest.FlatType, opt => opt.MapFrom(src => src.FlatType))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.PropertyStatus)).ReverseMap();

            //CreateMap<Property, PropertyDTO>()
            //    .ForMember(dest => dest.PropertyType, opt => opt.MapFrom(src => src.Type))
            //    .ForMember(dest => dest.FlatType, opt => opt.MapFrom(src => src.FlatType))
            //    .ForMember(dest => dest.PropertyStatus, opt => opt.MapFrom(src => src.Status));

        }
    }
}
