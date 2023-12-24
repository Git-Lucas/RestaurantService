using AutoMapper;
using ItemService.Data;
using ItemService.Dtos;
using ItemService.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace ItemService.Controllers;

[Route("api/item/[controller]")]
[ApiController]
public class RestaurantController(IItemRepository itemRepository, IMapper mapper) : ControllerBase
{
    [HttpGet]
    public ActionResult<IEnumerable<RestaurantReadDto>> GetAll()
    {
        IEnumerable<Restaurant> restaurants = itemRepository.GetAllRestaurants();

        IEnumerable<RestaurantReadDto> restaurantsReadDto = mapper.Map<IEnumerable<RestaurantReadDto>>(restaurants);

        return Ok(restaurantsReadDto);
    }

    [HttpPost]
    public ActionResult ReceiveRestaurantFromRestaurantService([FromBody] RestaurantReadDto restaurantReadDto)
    {
        Restaurant restaurant = mapper.Map<Restaurant>(restaurantReadDto);
        
        itemRepository.CreateRestaurant(restaurant);
        itemRepository.SaveChanges();
        
        return Ok();
    }
}
