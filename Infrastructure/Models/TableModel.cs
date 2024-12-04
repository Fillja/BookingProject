using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Models;

public class TableModel
{
    [Range (2, 16)]
    public int Size { get; set; }

    public bool IsBooked { get; set; }

    [Range(0, 16)]
    public int Vegan { get; set; }

    [Range(0, 16)]
    public int Vegetarian { get; set; }

    [Range(0, 16)]
    public int Milk { get; set; }

    [Range(0, 16)]
    public int Eggs { get; set; }

    [Range(0, 16)]
    public int Gluten { get; set; }
}
