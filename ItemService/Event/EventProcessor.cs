using AutoMapper;
using ItemService.Data;
using ItemService.Dtos;
using ItemService.Models;
using System.Text.Json;

namespace ItemService.Event;

public class EventProcessor(IMapper mapper, IServiceScopeFactory serviceScopeFactory) : IEventProcessor
{
    public void Process(string message)
    {
        using var scope = serviceScopeFactory.CreateScope();
        IItemRepository itemRepository = scope.ServiceProvider.GetRequiredService<IItemRepository>();

        RestaurantReadDto restaurantReadDto = JsonSerializer.Deserialize<RestaurantReadDto>(message)
            ?? throw new Exception("It was not possible to convert the message.");

        Restaurant restaurant = mapper.Map<Restaurant>(restaurantReadDto);

        if (!itemRepository.ExistsExternalRestaurant(restaurant.Id))
        {
            itemRepository.CreateRestaurant(restaurant);
        }
    }
}
