using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Repository.Models;

public class Product : IEquatable<Product>
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
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

    public override bool Equals(object? obj)
    {
        return Equals(obj as Product);
    }

    public bool Equals(Product? other)
    {
        return other is not null &&
               Id == other.Id;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id, Name);
    }
}
