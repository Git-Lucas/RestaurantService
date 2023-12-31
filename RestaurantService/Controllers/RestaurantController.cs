﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RestaurantService.Data;
using RestaurantService.Dtos;
using RestaurantService.Models;
using RestaurantService.RabbitMq;

namespace RestaurantService.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RestaurantController(
    IRestaurantRepository restaurantRepository,
    IMapper mapper,
    IRabbitMqClient rabbitMqClient) : ControllerBase
{
    [HttpGet]
    public ActionResult<IEnumerable<RestaurantReadDto>> GetAll()
    {
        IEnumerable<Restaurant> restaurants = restaurantRepository.GetAll();

        return Ok(mapper.Map<IEnumerable<RestaurantReadDto>>(restaurants));
    }

    [HttpGet("{id}", Name = nameof(GetById))]
    public ActionResult<RestaurantReadDto> GetById(int id)
    {
        Restaurant restaurant = restaurantRepository.GetById(id);

        return Ok(mapper.Map<RestaurantReadDto>(restaurant));
    }

    [HttpPost]
    public ActionResult<RestaurantReadDto> Create([FromBody] RestaurantCreateDto restaurantCreateDto)
    {
        Restaurant restaurant = mapper.Map<Restaurant>(restaurantCreateDto);
        restaurantRepository.Create(restaurant);

        RestaurantReadDto restaurantReadDto = mapper.Map<RestaurantReadDto>(restaurant);

        rabbitMqClient.SendRestaurant(restaurantReadDto);

        return CreatedAtRoute(
            routeName: nameof(GetById),
            routeValues: new { restaurantReadDto.Id },
            value: restaurantReadDto);
    }

    [HttpDelete]
    public ActionResult DeleteAll()
    {
        restaurantRepository.DeleteAll();

        return Ok();
    }
}
