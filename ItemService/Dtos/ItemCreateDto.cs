namespace ItemService.Dtos;

public record ItemCreateDto
{
    public required string Name { get; set; }
    public required double Price { get; set; }
}
