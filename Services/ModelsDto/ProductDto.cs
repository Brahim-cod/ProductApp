using System.ComponentModel.DataAnnotations;

namespace Services.ModelsDto;

public class ProductDto
{
    public int ProductID { get; set; }
    public string ProductName { get; set; }
    public string ProductDescription { get; set; }
    public string ProductImage { get; set; }
    public double ProductPrice { get; set; }
    public int ProductQuantity { get; set; }
    public string ProductCategoryName { get; set; }
}
