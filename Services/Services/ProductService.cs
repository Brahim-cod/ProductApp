using AutoMapper;
using Repository.Models;
using Repository.Repository;
using Repository.UnitOfWork;
using Shared.ModelsDto;
using Shared.Services;
using System.Diagnostics.CodeAnalysis;

namespace Services.Services;

public class ProductService : IProductService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public ProductService(IUnitOfWork unitOfWork, IMapper mapper/*, IRepository<Product, int> productReposidtory*/)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    public async Task<ProductDto> Create(CreateProductDto entity)
    {
        var product = _mapper.Map<Product>(entity);
        var productCreated = await _unitOfWork.Products.CreateAsync(product);
        await _unitOfWork.CompleteAsync();
        return _mapper.Map<ProductDto>(productCreated);
    }

    public async Task<IReadOnlyCollection<ProductDto>> GetAll()
    {
        var list = await _unitOfWork.Products.GetAllAsync();
        return list.Select(_mapper.Map<ProductDto>).ToList();
    }

    public async Task<IReadOnlyCollection<ProductDto>> GetAllByName(string name)
    {
        var list = await _unitOfWork.Products.GetAllAsync((product => product.Name.Contains(name.Trim())));
        return list.Select(_mapper.Map<ProductDto>).ToList();
    }

    [return: MaybeNull]
    public async Task<ProductDto> GetByID(int id)
    {
        var product = await _unitOfWork.Products.GetAsync((product => product.Id.Equals(id)));
        return _mapper.Map<ProductDto>(product);
    }

    public async Task<bool> Remove(int id)
    {
        var product = await _unitOfWork.Products.GetAsync((product => product.Id.Equals(id)));
        if (product != null)
        {
            await _unitOfWork.Products.RemoveAsync(product);
            await _unitOfWork.CompleteAsync();
            return true;
        }
        return false;
    }

    public async Task<bool> Update(UpdateProductDto entity)
    {
        await _unitOfWork.Products.UpdateAsync(_mapper.Map<Product>(entity));
        await _unitOfWork.CompleteAsync();
        return true;
    }
}
