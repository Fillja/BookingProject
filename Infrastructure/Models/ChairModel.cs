using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Models;

public class ChairModel
{
    public string? Name { get; set; }
    public string? RestaurantName { get; set; }
    public bool Vegan { get; set; }
    public bool Vegetarian { get; set; }
    public bool Gluten { get; set; }
    public bool Milk { get; set; }
    public bool Egg { get; set; }
}
