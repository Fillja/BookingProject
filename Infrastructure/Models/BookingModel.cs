using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Models;

public class BookingModel
{
    public string? Id { get; set; }
    [Required]
    public DateTime CreatedDate { get; set; } = DateTime.Now;
    [Required]
    public DateTime BookingStartTime { get; set; } = DateTime.Now;
    [Required]
    public DateTime BookingEndTime { get; set; } = DateTime.Now;
    [Required]
    public string BookerName { get; set; } = null!;
    [Required]
    public string BookerEmail { get; set; } = null!;
    [Required]
    public string BookerPhone { get; set; } = null!;
    public int Vegan { get; set; }
    public int Vegetarian { get; set; }
    public int Lactose { get; set; }
    public int Milk { get; set; }
    public int Eggs { get; set; }
    public int Gluten { get; set; }
    public string? SpecialRequests { get; set; }
    public string TableId { get; set; } = null!;
    public string? TableName { get; set; }
}
