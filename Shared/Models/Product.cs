using System.ComponentModel.DataAnnotations;

namespace Shared.Models;

public class Product
{
    public int Id { get; set; }

    [Required]
    public string Name { get; set; }

    public string Description { get; set; }
    public string Image { get; set; }

    [Required]
    [Range(0, double.MaxValue)]
    public double Price { get; set; }
    [Required]
    [Range(0, int.MaxValue)]
    public int Quantity { get; set; }
    [Required]
    public int CategoryId { get; set; }
    public Category Category { get; set; }
}
