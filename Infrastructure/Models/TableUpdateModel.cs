using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Models;

public class TableUpdateModel
{
    public string? Name { get; set; }

    [Range(2, 16)]
    public int Size { get; set; }
    public bool IsBooked { get; set; }
}
