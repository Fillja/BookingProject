namespace BookingBackOffice.Models.Home;

public class HomeViewModel
{
    public string Title { get; set; } = "Home";
    public string RestaurantName { get; set; } = null!;
    public int Bookings { get; set; }
    public int Seatings { get; set; }
    public string? ErrorMessage { get; set; }
}
