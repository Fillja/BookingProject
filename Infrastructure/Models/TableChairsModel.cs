namespace Infrastructure.Models;

public class TableChairsModel
{
    public TableModel Table { get; set; } = new TableModel();
    public IEnumerable<ChairModel> Chairs { get; set; } = [];
}
