﻿namespace ItemService.Dtos;

public record ItemReadDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public required double Price { get; set; }
    public int RestaurantId { get; set; }
}
