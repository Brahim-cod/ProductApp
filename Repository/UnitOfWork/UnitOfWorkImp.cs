using Repository.Context;
using Repository.Models;
using Repository.Repository;
using Repository.Repository.EFRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.UnitOfWork;

public class UnitOfWorkImp : IUnitOfWork
{
    private readonly AppDbContext _context;
    public UnitOfWorkImp(
        AppDbContext context, 
        IRepository<Product, int> productRepository,
        IRepository<Category, int> categoryRepository
        )
    {
        _context = context;
        Products = productRepository;
        Categories = categoryRepository;
    }

    public IRepository<Product, int> Products { get; }

    public IRepository<Category, int> Categories { get; }

    public async Task<int> CompleteAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}
