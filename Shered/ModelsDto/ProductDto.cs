using Shered.Helpers;
using System.ComponentModel.DataAnnotations;

namespace Shared.ModelsDto;

public class ProductDto
{
    public int ProductID { get; set; }

    [Required(ErrorMessage = "Product name is required")]
    public string ProductName { get; set; }

    [Required(ErrorMessage = "Product description is required")]
    [MinWords(50, ErrorMessage = "Product description must contain at least 50 words.")]
    public string ProductDescription { get; set; }

    [Required(ErrorMessage = "Product Image Url is required")]
    [Url(ErrorMessage = "Invalid URL format for product image")]
    public string ProductImage { get; set; }

    [Required(ErrorMessage = "Product price is required")]
    [Range(1, double.MaxValue, ErrorMessage = "Product price must be a positive value")]
    public double ProductPrice { get; set; }

    [Required(ErrorMessage = "Product quantity is required")]
    [Range(1, int.MaxValue, ErrorMessage = "Product quantity must be a non-negative value")]
    public int ProductQuantity { get; set; }

    [Required(ErrorMessage = "Product category ID is required")]
    public int ProductCategoryID { get; set; }

    public string ProductCategoryName { get; set; }
}
