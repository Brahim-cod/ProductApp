using AutoMapper;
using Repository.Models;
using Repository.UnitOfWork;
using Services.ModelsDto;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        return _mapper.Map<ProductDto>(productCreated);
    }

    public async Task<IReadOnlyCollection<ProductDto>> GetAll()
    {
        var list = await _unitOfWork.Products.GetAllAsync();
        return list.Select(_mapper.Map<ProductDto>).ToList();
    }

    public IReadOnlyCollection<ProductDto> GetAllByName()
    {
        throw new NotImplementedException();
    }

    [return: MaybeNull]
    public ProductDto GetByID()
    {
        throw new NotImplementedException();
    }

    public void Remove(int id)
    {
        throw new NotImplementedException();
    }

    public void Update(UpdateProductDto entity)
    {
        throw new NotImplementedException();
    }
}
