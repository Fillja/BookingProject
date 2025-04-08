using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Models;

public class RestaurantModel
{
    public string Id { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string Location { get; set; } = null!;
}
