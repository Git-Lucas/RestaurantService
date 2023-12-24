using AutoMapper;
using ItemService.Dtos;
using ItemService.Models;

namespace ItemService.Profiles;

public class ItemProfile : Profile
{
	public ItemProfile()
	{
		CreateMap<Restaurant, RestaurantReadDto>();
		CreateMap<RestaurantReadDto, Restaurant>()
			.ForMember(dest => dest.ExternalId, opt => opt.MapFrom(src => src.Id));
		CreateMap<ItemCreateDto, Item>();
		CreateMap<Item, ItemReadDto>();
	}
}
