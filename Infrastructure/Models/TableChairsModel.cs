using Infrastructure.Entities;

namespace Infrastructure.Models;

public class TableChairsModel
{
    public string? Name { get; set; }
    public TableEntity Table { get; set; } = new TableEntity();
    public IEnumerable<ChairEntity> Chairs { get; set; } = [];
}
