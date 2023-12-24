using System.ComponentModel.DataAnnotations;

namespace ItemService.Models;

public class Item
{
    [Key]
    public required int Id { get; set; }
    public required string Name { get; set; }
    public required double Price { get; set; }
    public required int RestaurantId { get; set; }
    public Restaurant? Restaurant { get; set; }
}
