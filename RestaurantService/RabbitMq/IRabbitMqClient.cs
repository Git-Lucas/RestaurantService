using RestaurantService.Dtos;

namespace RestaurantService.RabbitMq;

public interface IRabbitMqClient
{
    void SendRestaurant(RestaurantReadDto restaurantReadDto);
}