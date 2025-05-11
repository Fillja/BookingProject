using Infrastructure.Models;

namespace BookingBackOffice.Models;

public class TableViewModel
{
    public string Title { get; set; } = "Tables";
    public List<TableModel> Tables { get; set; } = [];
    public string? DisplayMessage { get; set; }
    public string? ErrorMessage { get; set; }
}
