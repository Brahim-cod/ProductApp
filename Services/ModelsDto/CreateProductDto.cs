using System.ComponentModel.DataAnnotations;

namespace Services.ModelsDto;

public class CreateProductDto
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
