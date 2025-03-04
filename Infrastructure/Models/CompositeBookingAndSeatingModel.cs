namespace Infrastructure.Models;

public class CompositeBookingAndSeatingModel
{
    public BookingCreateModel Booking { get; set; } = null!;

    public SeatingBookingModel Seating { get; set; } = null!;
}
