using ItemService.Models;

namespace ItemService.Data;

public interface IItemRepository
{
    IEnumerable<Restaurant> GetAllRestaurants();
    void CreateRestaurant(Restaurant restaurant);
    bool ExistsRestaurant(int restaurantId);
    bool ExistsExternalRestaurant(int externalRestaurantId);

    IEnumerable<Item> GetAllByRestaurantId(int restaurantId);
    Item Get(int restaurantId, int itemId);
    void Create(int restaurantId, Item item);

    void SaveChanges();
}