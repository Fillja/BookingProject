using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Models;

public class TableModel
{
    public string? Name { get; set; }
    public string RestaurantName { get; set; } = null!;

    [Range (2, 16)]
    public int Size { get; set; }
    public bool IsBooked { get; set; }
}
