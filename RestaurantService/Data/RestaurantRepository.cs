﻿using RestaurantService.Models;

namespace RestaurantService.Data;

public class RestaurantRepository(AppDbContext context) : IRestaurantRepository
{
    public void Create(Restaurant restaurant)
    {
        ArgumentNullException.ThrowIfNull(restaurant);

        context.Restaurants.Add(restaurant);
    }

    public IEnumerable<Restaurant> GetAll()
    {
        return context.Restaurants.ToList();
    }

    public Restaurant GetById(int id)
    {
        Restaurant restaurant = context.Restaurants.FirstOrDefault(x => x.Id == id)
            ?? throw new Exception($"The {nameof(Restaurant)} was not found.");

        return restaurant;
    }

    public void SaveChanges()
    {
        context.SaveChanges();
    }
}
