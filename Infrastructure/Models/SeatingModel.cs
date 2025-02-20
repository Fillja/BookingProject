using Infrastructure.Entities;

namespace Infrastructure.Models;

public class SeatingModel
{
    public string? Name { get; set; }

    public TableEntity Table { get; set; } = null!;

    public List<ChairEntity> Chairs { get; set; } = [];
}
