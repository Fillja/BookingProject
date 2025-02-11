using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Entities;

public class ChairEntity
{
    [Key]
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string? Name { get; set; }
    public bool Vegan { get; set; }
    public bool Vegetarian { get; set; }
    public bool Milk { get; set; }
    public bool Eggs { get; set; }
    public bool Gluten { get; set; }

    public string RestaurantId { get; set; } = null!;

    [ForeignKey("RestaurantId")]
    public RestaurantEntity Restaurant { get; set; } = null!;
}
