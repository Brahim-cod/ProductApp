using Shared.ModelsDto;

namespace Shared.Services;

public interface IProductService
{
    Task<ProductDto> Create(CreateProductDto entity);
    Task<IReadOnlyCollection<ProductDto>> GetAll();
    Task<IReadOnlyCollection<ProductDto>> GetAllByName(string name);
    Task<ProductDto> GetByID(int id);
    Task<bool> Remove(int id);
    Task<bool> Update(UpdateProductDto entity);
}