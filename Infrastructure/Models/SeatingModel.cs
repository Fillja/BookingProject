namespace Infrastructure.Models;

public class SeatingModel
{
    public string? Name { get; set; }
    public string TableId { get; set; } = null!;
    public IEnumerable<string> ChairIds { get; set; } = [];
}
