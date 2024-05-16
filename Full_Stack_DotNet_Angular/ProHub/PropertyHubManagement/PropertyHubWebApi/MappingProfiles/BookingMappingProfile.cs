using AutoMapper;
using DATA_ACCESS_LAYER.DTOs;
using DATA_ACCESS_LAYER.Models;

namespace PropertyHubWebApi.MappingProfiles
{
    public class BookingMappingProfile : Profile
    {
        public BookingMappingProfile() 
        {
            CreateMap<Booking, BookingDTO>()
                .ForMember(dest => dest.Status,
                    opt => opt.MapFrom(src => src.Status.ToString()));

            CreateMap<BookingDTO, Booking>()
                .ForMember(dest => dest.Status,
                    opt => opt.MapFrom(src => src.Status.ToString()));

            // CreateMap<Booking, BookingDTO>().ReverseMap();
            //// CreateMap<Booking, BookingDTO>();

        }
    }
}
