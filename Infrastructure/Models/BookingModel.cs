using Infrastructure.Entities;
using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Models;

public class BookingModel
{
    public DateTime BookingDate { get; set; } = DateTime.Now;
    public DateTime BookingStartTime { get; set; }

    [Required]
    public string BookerName { get; set; } = null!;

    [Required(ErrorMessage = "Email address is required.")]
    [RegularExpression("^[^@\\s]+@[^@\\s]+\\.[^@\\s]{2,}$")]
    public string BookerEmail { get; set; } = null!;

    [Required]
    [RegularExpression("^(?:\\+46|0)7\\d{8}$")]
    public string BookerPhone { get; set; } = null!;

    public string? SpecialRequests { get; set; }

    public string SeatingId { get; set; } = null!;

}
