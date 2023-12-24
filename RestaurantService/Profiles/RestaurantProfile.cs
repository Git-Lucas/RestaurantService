using AutoMapper;
using RestaurantService.Dtos;
using RestaurantService.Models;

namespace RestaurantService.Profiles;

public class RestaurantProfile : Profile
{
    public RestaurantProfile()
    {
        CreateMap<Restaurant, RestaurantReadDto>();
        CreateMap<RestaurantCreateDto, Restaurant>();
    }
}
