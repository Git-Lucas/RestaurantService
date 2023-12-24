using RestaurantService.Dtos;
using System.Text;
using System.Text.Json;

namespace RestaurantService.ItemServiceCommunication;

public class ItemServiceHttpClient(HttpClient httpClient, IConfiguration configuration) : IItemServiceHttpClient
{
    public async void SendRestaurantToItemService(RestaurantReadDto restaurantReadDto)
    {
        StringContent content = new(
            content: JsonSerializer.Serialize(restaurantReadDto),
            encoding: Encoding.UTF8,
            mediaType: "application/json");

        await httpClient.PostAsync(configuration["ItemServiceAddress"], content);
    }
}
