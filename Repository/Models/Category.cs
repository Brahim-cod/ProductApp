using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Repository.Models;

public class Category : IEquatable<Category>
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    public string Name { get; set; }
    public string ImageUrl { get; set; }

    public ICollection<Product> Products { get; set; }

    public override bool Equals(object? obj)
    {
        return Equals(obj as Category);
    }

    public bool Equals(Category? other)
    {
        return other is not null &&
               Id == other.Id;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id, Name);
    }
}