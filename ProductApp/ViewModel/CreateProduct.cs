using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

namespace ProductApp.ViewModel;

public class CreateProduct
{
    [Required]
    public string ProductName { get; set; }
    [Required]
    public string ProductDescription { get; set; }
    public string ProductImage { get; set; }
    [Required]
    [Range(0, double.MaxValue)]
    public double ProductPrice { get; set; }
    [Required]
    [Range(0, int.MaxValue)]
    public int ProductQuantity { get; set; }
    [Required]
    public int ProductCategoryID { get; set; }
}
