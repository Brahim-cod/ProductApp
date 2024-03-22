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

public class OrderProductRepository : IRepository<OrderProduct, (int, int)>
{
    private readonly AppDbContext _dbContext;

    public OrderProductRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<OrderProduct> CreateAsync(OrderProduct entity)
    {
        if (entity == null) { throw new ArgumentNullException(nameof(entity)); }

        await _dbContext.OrderProducts.AddAsync(entity);
        //await _dbContext.SaveChangesAsync();

        return entity;
    }

    public async Task<IReadOnlyCollection<OrderProduct>> GetAllAsync()
    {
        return await _dbContext.OrderProducts.ToListAsync();
    }

    public async Task<IReadOnlyCollection<OrderProduct>> GetAllAsync(Expression<Func<OrderProduct, bool>> filter)
    {
        return await _dbContext.OrderProducts.Where(filter)
            .Include(op => op.Order)
            .Include(op => op.Product)
            .ToListAsync();
    }

    [return: MaybeNull]
    public async Task<OrderProduct> GetAsync(Expression<Func<OrderProduct, bool>> filter)
    {
        return await _dbContext.OrderProducts.FirstOrDefaultAsync(filter);
    }

    public async Task RemoveAsync(OrderProduct entity)
    {
        _dbContext.OrderProducts.Remove(entity);
    }

    public async Task UpdateAsync(OrderProduct entity)
    {
        if (entity == null) { throw new ArgumentNullException(nameof(entity)); }

        // Find the tracked entity in the context by its primary key
        var trackedEntity = await GetAsync(orderProduct => orderProduct.Equals(entity));

        if (trackedEntity == null)
        {
            throw new InvalidOperationException($"Order with ID {entity.OrderId} and Product with {entity.ProductId} not found.");
        }

        // Update properties of the tracked entity with the new values
        _dbContext.Entry(trackedEntity).CurrentValues.SetValues(entity);

        //await _dbContext.SaveChangesAsync();
    }
    
}
