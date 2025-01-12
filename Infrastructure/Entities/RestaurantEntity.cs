using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Entities;

public class RestaurantEntity
{
    [Key]
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Name { get; set; } = null!;
    public string Location { get; set; } = null!;

    public virtual ICollection<TableEntity> Tables { get; set; } = new List<TableEntity>();
}
