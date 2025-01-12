using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Entities;

//public class SeatingEntity
//{
//    [Key]
//    public string Id { get; set; } = Guid.NewGuid().ToString();

//    public string TableChairId { get; set; } = null!;

//    [ForeignKey("TableChairId")]
//    public TableChairEntity TableChair { get; set; } = null!;

//    public string RestaurantId { get; set; } = null!;

//    [ForeignKey("RestaurantId")]
//    public RestaurantEntity Restaurant { get; set; } = null!;

//    public virtual ICollection<BookingEntity> Bookings { get; set; } = new List<BookingEntity>();
//}
