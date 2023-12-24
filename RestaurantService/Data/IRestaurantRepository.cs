using RestaurantService.Models;

namespace RestaurantService.Data;

public interface IRestaurantRepository
{
    void SaveChanges();
    void Create(Restaurant restaurant);
    IEnumerable<Restaurant> GetAll();
    Restaurant GetById(int id);
}
