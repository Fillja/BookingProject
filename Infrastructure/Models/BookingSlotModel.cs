namespace Infrastructure.Models;

public class BookingSlotModel
{
    public string BookerName { get; set; } = null!;
    public string BookerEmail { get; set; } = null!;
    public DateTime BookingStartTime { get; set; }
    public DateTime BookingEndTime { get; set; }
}