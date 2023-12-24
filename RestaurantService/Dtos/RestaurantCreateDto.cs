namespace RestaurantService.Dtos;

public record RestaurantCreateDto
{
    public required string Name { get; set; }
    public required string Address { get; set; }
    public required string Site { get; set; }
}
