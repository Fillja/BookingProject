namespace Infrastructure.Entities;

public class TableEntity
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public int Size { get; set; }
    public bool IsBooked { get; set; }
    public int Vegan { get; set; }
    public int Vegetarian { get; set; }
    public int Milk { get; set; }
    public int Eggs { get; set; }
    public int Gluten { get; set; }

    public RestaurantEntity Restaurant { get; set; } = null!;

    public virtual ICollection<OrderEntity>? Orders { get; set; }
}
