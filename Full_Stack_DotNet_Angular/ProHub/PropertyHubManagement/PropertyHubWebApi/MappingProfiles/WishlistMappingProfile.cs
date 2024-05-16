using AutoMapper;
using DATA_ACCESS_LAYER.Models;
using DATA_ACCESS_LAYER.DTOs;

namespace PropertyHubWebApi.MappingProfiles
{
    public class WishlistMappingProfile : Profile
    {
         public WishlistMappingProfile ()
        {
            CreateMap<Wishlist, WishlistDTO>().ReverseMap();
        }
    }
}
