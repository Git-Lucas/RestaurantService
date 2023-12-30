using System.ComponentModel.DataAnnotations;

namespace ItemService.Models;

public class Restaurant
{
    [Key]
    public required int Id { get; set; }
    public required int ExternalId { get; set; }
    public required string Name { get; set; }
    public required string Address { get; set; }
    public required string Site { get; set; }
    public ICollection<Item> Items { get; set; } = new List<Item>();
}
