namespace Infrastructure.Models;

public class CreateSeatingModel
{
    public string? Name { get; set; }
    public string TableId { get; set; } = null!;
    public IEnumerable<string> ChairIds { get; set; } = [];
}
