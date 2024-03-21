using Repository.Models;
using Repository.Repository;
using Repository.Repository.EFRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.UnitOfWork;

public interface IUnitOfWork : IDisposable
{
    // Repositories
    IRepository<Product, int> Products { get; }
    IRepository<Category, int> Categories { get; }

    Task<int> CompleteAsync();
}
