namespace Infrastructure.Models;

//DTO in TableModel that references the BookingEntity
public class BookingSlotModel
{
    public string BookerName { get; set; } = null!;
    public string BookerEmail { get; set; } = null!;
    public DateTime BookingStartTime { get; set; }
    public DateTime BookingEndTime { get; set; }
}