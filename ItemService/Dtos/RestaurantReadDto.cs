﻿namespace ItemService.Dtos;

public class RestaurantReadDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string Site { get; set; } = string.Empty;
}
