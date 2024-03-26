using Shared.ModelsDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Services;

public interface ICategoryService
{
    Task<IReadOnlyCollection<CategoryDto>> GetAll();
    Task<CategoryDto> GetByID(int id);
}
