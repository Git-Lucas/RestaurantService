using RestaurantService.Dtos;

namespace RestaurantService.ItemServiceCommunication;

public interface IItemServiceHttpClient
{
    void SendRestaurantToItemService(RestaurantReadDto restaurantReadDto);
}
