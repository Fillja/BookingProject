namespace Infrastructure.Models;

public class CompositeBookingAndSeatingModel
{
    public BookingModel Booking { get; set; } = null!;

    public SeatingBookingModel Seating { get; set; } = null!;
}
