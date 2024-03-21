using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.ModelsDto;

public class UpdateProductDto
{
    public int ProductID { get; set; }
    public string ProductName { get; set; }
    public string ProductDescription { get; set; }
    public string ProductImage { get; set; }
    public double ProductPrice { get; set; }
    public int ProductQuantity { get; set; }
    public int ProductCategoryID { get; set; }
}
