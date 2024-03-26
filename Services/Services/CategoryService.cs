using AutoMapper;
using Repository.UnitOfWork;
using Shared.ModelsDto;
using Shared.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services;

public class CategoryService : ICategoryService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public CategoryService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    public async Task<IReadOnlyCollection<CategoryDto>> GetAll()
    {
        var list = await _unitOfWork.Categories.GetAllAsync();
        return list.Select(_mapper.Map<CategoryDto>).ToList();
    }

    public async Task<CategoryDto> GetByID(int id)
    {
        var category = await _unitOfWork.Categories.GetAsync(cat => cat.Id == id);
        return _mapper.Map<CategoryDto>(category);
    }
}
