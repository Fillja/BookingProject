using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Entities;

public class TableEntity
{
    [Key]
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string? Name { get; set; }
    public int Size { get; set; }
    public bool IsBooked { get; set; }

    public string RestaurantId { get; set; } = null!;

    [ForeignKey("RestaurantId")]
    public RestaurantEntity Restaurant { get; set; } = null!;

    public virtual ICollection<TableChairEntity> TablesChairs { get; set; } = new List<TableChairEntity>();
}
