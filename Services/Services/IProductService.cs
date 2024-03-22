using Services.ModelsDto;

namespace Services.Services
{
    public interface IProductService
    {
        Task<ProductDto> Create(CreateProductDto entity);
        Task<IReadOnlyCollection<ProductDto>> GetAll();
        Task<IReadOnlyCollection<ProductDto>> GetAllByName(string name);
        Task<ProductDto> GetByID(int id);
        Task Remove(int id);
        Task Update(UpdateProductDto entity);
    }
}