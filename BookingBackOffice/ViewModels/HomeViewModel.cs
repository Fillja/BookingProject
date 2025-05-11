namespace BookingBackOffice.Models;

public class HomeViewModel
{
    public string Title { get; set; } = "Home";
    public string RestaurantName { get; set; } = null!;
    public int Bookings { get; set; }
    public int Tables { get; set; }
    public string? ErrorMessage { get; set; }
}
