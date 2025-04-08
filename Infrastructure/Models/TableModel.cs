using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Models;

public class TableModel
{
    public string? Id { get; set; }
    public string? Name { get; set; }
    public string RestaurantId { get; set; } = null!;

    [Range (2, 16)]
    public int Size { get; set; }
    public List<BookingSlotModel> Bookings { get; set; } = [];
}
