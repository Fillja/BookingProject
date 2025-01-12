using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Entities;

public class BookingEntity
{
    [Key]
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public DateTime CreatedDate { get; set; } = DateTime.Now;
    public string BookerName { get; set; } = null!;
    public string BookerEmail { get; set; } = null!;
    public string BookerPhone { get; set; } = null!;

    public string TableChairId { get; set; } = null!;

    [ForeignKey("TableChairId")]
    public TableChairEntity TableChair { get; set; } = null!;
}
