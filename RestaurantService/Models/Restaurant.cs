using System.ComponentModel.DataAnnotations;

namespace RestaurantService.Models;

public class Restaurant
{
    [Key]
    public required int Id { get; set; }
    public required string Name { get; set; }
    public required string Address { get; set; }
    public required string Site { get; set; }
}
