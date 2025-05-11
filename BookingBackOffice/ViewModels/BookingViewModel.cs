using Infrastructure.Models;

namespace BookingBackOffice.Models;

public class BookingViewModel
{
    public string Title { get; set; } = "Bookings";
    public List<BookingModel> Bookings { get; set; } = [];
    public List<TableModel> Tables { get; set; } = [];
    public string? DisplayMessage { get; set; }
    public string? ErrorMessage { get; set; }
}
