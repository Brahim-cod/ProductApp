using Microsoft.EntityFrameworkCore;
using Repository.Context;
using Repository.Extensions;
using Repository.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository.EFRepository;

public class OrderRepository : IRepository<Order, int>
{
    private readonly AppDbContext _dbContext;

    public OrderRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<Order> CreateAsync(Order entity)
    {
        if (entity == null) { throw new ArgumentNullException(nameof(entity)); }

        await _dbContext.Orders.AddAsync(entity);

        await _dbContext.SaveChangesAsync();

        return entity;
    }

    public async Task<IReadOnlyCollection<Order>> GetAllAsync()
    {
        return await _dbContext.Orders.ToListAsync();
    }

    public async Task<IReadOnlyCollection<Order>> GetAllAsync(Expression<Func<Order, bool>> filter)
    {
        return await _dbContext.Orders.Where(filter).ToListAsync();
    }

    [return: MaybeNull]
    public async Task<Order> GetAsync(Expression<Func<Order, bool>> filter)
    {
        return await _dbContext.Orders.FirstOrDefaultAsync(filter);
    }

    public async Task RemoveAsync(Order entity)
    {
        _dbContext.Orders.Remove(entity);
    }

    public async Task UpdateAsync(Order entity)
    {
        if (entity == null) { throw new ArgumentNullException(nameof(entity)); }

        // Find the tracked entity in the context by its primary key
        var trackedEntity = await GetAsync(order => order.Equals(entity));

        if (trackedEntity == null)
        {
            throw new InvalidOperationException($"Order with ID {entity.Id} not found.");
        }

        // Update properties of the tracked entity with the new values
        _dbContext.Entry(trackedEntity).CurrentValues.SetValues(entity);

        //await _dbContext.SaveChangesAsync();
    }
}
