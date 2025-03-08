namespace Infrastructure.Models;

public class CompositeBookingAndSeatingModel
{
    public BookingMinimalModel Booking { get; set; } = null!;

    public SeatingBookingModel Seating { get; set; } = null!;
}
