namespace Infrastructure.Models;

public class SeatingBookingModel
{
    public string TableId { get; set; } = null!;

    public List<ChairUpdateModel> Chairs { get; set; } = [];
}
