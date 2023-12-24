using AutoMapper;
using ItemService.Data;
using ItemService.Dtos;
using ItemService.Models;
using Microsoft.AspNetCore.Mvc;

namespace ItemService.Controllers;

[Route("api/item/restaurant/{restaurantId}/[controller]")]
[ApiController]
public class ItemController(IItemRepository itemRepository, IMapper mapper) : ControllerBase
{
    [HttpGet]
    public ActionResult<IEnumerable<ItemReadDto>> GetAllItemsByRestaurantId([FromBody] int restaurantId)
    {
        if (!itemRepository.ExistsRestaurant(restaurantId))
        {
            return NotFound();
        }

        IEnumerable<Item> items = itemRepository.GetAllByRestaurantId(restaurantId);
        IEnumerable<ItemReadDto> itemsReadDto = mapper.Map<IEnumerable<ItemReadDto>>(items);

        return Ok(itemsReadDto);
    }

    [HttpGet("{itemId}", Name = nameof(GetItem))]
    public ActionResult<ItemReadDto> GetItem(int restaurantId, [FromRoute] int itemId)
    {
        if (!itemRepository.ExistsRestaurant(restaurantId))
        {
            return NotFound();
        }

        Item item = itemRepository.Get(restaurantId, itemId);
        ItemReadDto itemReadDto = mapper.Map<ItemReadDto>(item);

        return Ok(itemReadDto);
    }

    [HttpPost]
    public ActionResult<ItemReadDto> CreateItem(int restaurantId, [FromBody] ItemCreateDto itemCreateDto)
    {
        if (!itemRepository.ExistsRestaurant(restaurantId))
        {
            return NotFound();
        }

        Item item = mapper.Map<Item>(itemCreateDto);

        itemRepository.Create(restaurantId, item);
        itemRepository.SaveChanges();

        ItemReadDto itemReadDto = mapper.Map<ItemReadDto>(item);

        return CreatedAtRoute(
            routeName: nameof(GetItem), 
            routeValues: new { restaurantId, itemId = itemReadDto.Id },
            value: itemReadDto);
    }
}
