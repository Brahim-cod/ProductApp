using Microsoft.AspNetCore.Components;
using Shared.ModelsDto;
using Shared.Services;

namespace ProductWasm.Pages.ProductPages;

public partial class Product
{
    [Inject]
    private IProductService _productService {  get; set; }
    [Inject]
    private ICategoryService _categoryService { get; set; }
    [Parameter]
    public int? ProductId { get; set; }
    public ProductDto productDto { get; set; } = new ProductDto();
    private IEnumerable<CategoryDto> _categories = new List<CategoryDto>();
    protected override async Task OnParametersSetAsync()
    {
        if (ProductId.HasValue)
        {
            var product = await _productService.GetByID(ProductId.Value);
            if (product != null)
            {
                productDto = product;
            }
        }
        
    }

    protected override async Task OnInitializedAsync()
    {
        var categories = await _categoryService.GetAll();
        if (categories.Any())
        {
            _categories = categories;
        }
    }

    private async Task HandleValidSubmit()
    {
        if (ProductId.HasValue)
        {
            await UpdateProduct();
        }
        else
        {
            await CreateProduct();
        }
    }

    private async Task CreateProduct()
    {
        var createProduct = new CreateProductDto()
        {
            ProductName = productDto.ProductName,
            ProductDescription = productDto.ProductDescription,
            ProductCategoryID = productDto.ProductCategoryID,
            ProductImage = productDto.ProductImage,
            ProductPrice = productDto.ProductPrice,
            ProductQuantity = productDto.ProductQuantity
        };

        var result = await _productService.Create(createProduct);
        if (result != null)
        {
            // Product created successfully, redirect or show success message
        }
        else
        {
            // Handle error
        }
    }
    private async Task UpdateProduct()
    {
        var updateProduct = new UpdateProductDto()
        {
            ProductID = productDto.ProductID,
            ProductDescription = productDto.ProductDescription,
            ProductName = productDto.ProductName,
            ProductQuantity = productDto.ProductQuantity,
            ProductImage = productDto.ProductImage,
            ProductPrice = productDto.ProductPrice,
            ProductCategoryID = productDto.ProductCategoryID
        };

        var success = await _productService.Update(updateProduct);
        if (success)
        {
            // Product updated successfully, redirect or show success message
        }
        else
        {
            // Handle error
        }
    }

    private string GetSubmitButtonText()
    {
        return ProductId.HasValue ? "Update" : "Create";
    }

}
