using AutoMapper;
using Repository.Models;
using Repository.UnitOfWork;
using Services.ModelsDto;
using System.Diagnostics.CodeAnalysis;

namespace Services.Services;

public class ProductService : IProductService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public ProductService(IUnitOfWork unitOfWork, IMapper mapper)
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

    public async void Remove(int id)
    {
        var product = await GetByID(id);
        await _unitOfWork.Products.RemoveAsync(_mapper.Map<Product>(product));
        await _unitOfWork.CompleteAsync();
    }

    public async void Update(UpdateProductDto entity)
    {
        await _unitOfWork.Products.UpdateAsync(_mapper.Map<Product>(entity));
        await _unitOfWork.CompleteAsync();
    }
}
