﻿using AutoMapper;
using ItemService.Dtos;
using ItemService.Models;

namespace ItemService.Profiles;

public class ItemProfile : Profile
{
	public ItemProfile()
	{
		CreateMap<Restaurant, RestaurantReadDto>();
		CreateMap<RestaurantReadDto, Restaurant>();
		CreateMap<ItemCreateDto, Item>();
		CreateMap<Item, ItemReadDto>();
	}
}
