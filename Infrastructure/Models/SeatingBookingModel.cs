namespace Infrastructure.Models;

public class SeatingBookingModel
{
    public string TableId { get; set; } = null!;

    public List<ChairBookingModel> Chairs { get; set; } = [];
}
