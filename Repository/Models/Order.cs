using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Models;

public class Order : IEquatable<Order>
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    [Required]
    public DateTimeOffset CreateAt { get; set; }
    public double Amount { get; set; }
    public ICollection<OrderProduct> OrderProducts { get; set; }

    public override bool Equals(object? obj)
    {
        return Equals(obj as Order);
    }

    public bool Equals(Order? other)
    {
        return other is not null &&
               Id == other.Id;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id, CreateAt);
    }

}
