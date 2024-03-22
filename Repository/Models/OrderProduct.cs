using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Models;

public class OrderProduct : IEquatable<OrderProduct>
{
    public int OrderId { get; set; }
    public Order Order { get; set; }
    public int ProductId { get; set; }
    public Product Product { get; set; }

    public int Quantity { get; set; }

    public override bool Equals(object? obj)
    {
        return Equals(obj as OrderProduct);
    }

    public bool Equals(OrderProduct? other)
    {
        return other is not null &&
               OrderId == other.OrderId &&
               ProductId == other.ProductId;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(OrderId, ProductId);
    }
}
