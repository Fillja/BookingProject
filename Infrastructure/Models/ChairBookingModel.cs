namespace Infrastructure.Models;

public class ChairBookingModel
{
    public string ChairId { get; set; } = null!;
    public string? Name { get; set; }
    public bool Vegan { get; set; }
    public bool Vegetarian { get; set; }
    public bool Gluten { get; set; }
    public bool Milk { get; set; }
    public bool Egg { get; set; }
}