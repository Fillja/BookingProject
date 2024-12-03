using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Entities;

public class OrderEntity
{
    [Key]
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public DateTime CreatedDate { get; set; } = DateTime.Now;
    public string BookerName { get; set; } = null!;
    public string BookerEmail { get; set; } = null!;
    public string BookerPhone { get; set; } = null!;

    public TableEntity Table { get; set; } = null!;
}
