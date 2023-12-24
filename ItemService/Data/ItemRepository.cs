using ItemService.Models;

namespace ItemService.Data;

public class ItemRepository(AppDbContext context) : IItemRepository
{
    public void Create(int restaurantId, Item item)
    {
        item.RestaurantId = restaurantId;
        context.Items.Add(item);
    }

    public void CreateRestaurant(Restaurant restaurant)
    {
        context.Restaurants.Add(restaurant);
    }

    public bool ExistsRestaurant(int restaurantId)
    {
        return context.Restaurants.Any(x => x.Id == restaurantId);
    }

    public bool ExistsExternalRestaurant(int externalRestaurantId)
    {
        return context.Restaurants.Any(x => x.ExternalId == externalRestaurantId);
    }

    public Item Get(int restaurantId, int itemId)
    {
        return context.Items.FirstOrDefault(x => x.RestaurantId == restaurantId && x.Id == itemId)
            ?? throw new Exception($"The {nameof(Item)} was not found.");
    }

    public IEnumerable<Item> GetAllByRestaurantId(int restaurantId)
    {
        return context.Items.Where(x => x.RestaurantId == restaurantId);
    }

    public IEnumerable<Restaurant> GetAllRestaurants()
    {
        return context.Restaurants.ToList();
    }

    public void SaveChanges()
    {
        context.SaveChanges();
    }
}
