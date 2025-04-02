namespace Infrastructure.Models;

public class BookingModel
{
    public DateTime CreatedDate { get; set; }

    public DateTime BookingStartTime { get; set; }

    public DateTime BookingEndTime { get; set; }

    public string BookerName { get; set; } = null!;

    public string BookerEmail { get; set; } = null!;

    public string BookerPhone { get; set; } = null!;

    public string? SpecialRequests { get; set; }

}
