using Services.ModelsDto;
using System.Diagnostics.CodeAnalysis;

namespace Services.Services;

public interface IProductService
{
    ProductDto Create(CreateProductDto entity);
    IReadOnlyCollection<ProductDto> GetAll();
    IReadOnlyCollection<ProductDto> GetAllByName();
    [return: MaybeNull]
    ProductDto GetByID();
    void Remove(int id);
    void Update(UpdateProductDto entity);
}
