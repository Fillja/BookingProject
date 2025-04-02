using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Entities;

public class BookingEntity
{
    [Key]
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public DateTime CreatedDate { get; set; } = DateTime.Now;
    public DateTime BookingStartTime { get; set; }
    public DateTime BookingEndTime { get; set; }
    public string BookerName { get; set; } = null!;
    public string BookerEmail { get; set; } = null!;
    public string BookerPhone { get; set; } = null!;
    public int Vegan { get; set; }
    public int Vegetarian { get; set; }
    public int Lactose { get; set; }
    public int Milk { get; set; }
    public int Eggs { get; set; }
    public int Gluten { get; set; }
    public string? SpecialRequests { get; set; }

    public string TableId { get; set; } = null!;

    [ForeignKey("TableId")]
    public TableEntity? Table { get; set; }
}
