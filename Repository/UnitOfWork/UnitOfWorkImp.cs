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
        IRepository<Category, int> categoryRepository,
        IRepository<Order, int> orderRepository,
        IRepository<OrderProduct, (int, int)> orderProductsRepository
        )
    {
        _context = context;
        Products = productRepository;
        Categories = categoryRepository;
        Orders = orderRepository;
        OrderProducts = orderProductsRepository;
    }

    public IRepository<Product, int> Products { get; }

    public IRepository<Category, int> Categories { get; }
    public IRepository<Order, int> Orders { get; }
    public IRepository<OrderProduct, (int, int)> OrderProducts { get; }

    public async Task CompleteAsync()
    {
        await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}
