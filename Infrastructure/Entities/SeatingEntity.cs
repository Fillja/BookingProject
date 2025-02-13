using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Entities;

public class SeatingEntity
{
    [Key]
    public string Id { get; set; } = Guid.NewGuid().ToString();

    public string? Name { get; set; }

    public string TableId { get; set; } = null!;

    [ForeignKey("TableId")]
    public TableEntity Table { get; set; } = null!;

    public string ChairId { get; set; } = null!;

    [ForeignKey("ChairId")]
    public ChairEntity Chair { get; set; } = null!;
}
