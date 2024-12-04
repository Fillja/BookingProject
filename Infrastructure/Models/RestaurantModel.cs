using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Models;

public class RestaurantModel
{
    [Required]
    public string RestaurantName { get; set; } = null!;

    [Required]
    public string Location { get; set; } = null!;
}
