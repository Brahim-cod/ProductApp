using Microsoft.EntityFrameworkCore;
using Repository.Context;
using Repository.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository.EFRepository;

public class ProductRepository : IRepository<Product, int>
{
    private readonly AppDbContext _dbContext;

    public ProductRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }


    public async Task<Product> CreateAsync(Product entity)
    {
        if (entity == null) { throw new ArgumentNullException(nameof(entity)); }

        await _dbContext.Products.AddAsync(entity);
        //await _dbContext.SaveChangesAsync();

        return entity;
    }

    public async Task<IReadOnlyCollection<Product>> GetAllAsync() => await _dbContext.Products.Include(p => p.Category).ToListAsync();

    public async Task<IReadOnlyCollection<Product>> GetAllAsync(Expression<Func<Product, bool>> filter) => await _dbContext.Products.Include(p => p.Category).Where(filter).ToListAsync();

    [return: MaybeNull]
    public async Task<Product> GetAsync(Expression<Func<Product, bool>> filter) => await _dbContext.Products.Include(p => p.Category).FirstOrDefaultAsync(filter);

    public async Task RemoveAsync(Product entity)
    {
        _dbContext.Products.Remove(entity);
        //await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(Product entity)
    {
        if (entity == null) { throw new ArgumentNullException(nameof(entity)); }

        // Find the tracked entity in the context by its primary key
        var trackedEntity = _dbContext.Products.FirstOrDefault(product => product.Id == entity.Id);
        //var trackedEntity = await GetAsync((product => product.Id == entity.Id));

        if (trackedEntity == null)
        {
            throw new InvalidOperationException($"Product with ID {entity.Id} not found.");
        }

        // Update properties of the tracked entity with the new values
         _dbContext.Entry(trackedEntity).CurrentValues.SetValues(entity);

        //await _dbContext.SaveChangesAsync();
    }
}
