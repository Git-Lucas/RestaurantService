using AutoMapper;
using ItemService.Data;
using ItemService.Dtos;
using ItemService.Models;
using Microsoft.AspNetCore.Mvc;

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
}
