using Infrastructure.Models;

namespace BookingBackOffice.Models;

public class HomeViewModel
{
    public string Title { get; set; } = "Home";
    public string? RestaurantName { get; set; }
    public int Bookings { get; set; }
    public int Tables { get; set; }
    public IEnumerable<RestaurantModel> Restaurants { get; set; } = [];
    public BookingModel Booking { get; set; } = new BookingModel();
    public string? ErrorMessage { get; set; }
    public string? AdminMessage { get; set; }
}
