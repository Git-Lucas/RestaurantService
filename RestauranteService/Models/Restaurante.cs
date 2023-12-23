using System.ComponentModel.DataAnnotations;

namespace RestauranteService.Models;

public class Restaurante
{
    [Key]
    public required int Id { get; set; }
    public required string Name { get; set; }
    public required string Address { get; set; }
    public required Uri Site { get; set; }
}
