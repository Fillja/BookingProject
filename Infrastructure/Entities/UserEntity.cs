using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Entities;

public class UserEntity : IdentityUser
{
    public string RestaurantId { get; set; } = null!;

    [ForeignKey("RestaurantId")]
    public RestaurantEntity? Restaurant { get; set; } = null!;
}
