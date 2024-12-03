namespace Infrastructure.Entities;

public class RestaurantEntity
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Name { get; set; } = null!;
    public string Location { get; set; } = null!;

    public virtual ICollection<TableEntity>? Tables { get; set; }
}
