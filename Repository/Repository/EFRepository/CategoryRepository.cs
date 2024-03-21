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

public class CategoryRepository : IRepository<Category, int>
{
    private readonly AppDbContext _dbContext;

    public CategoryRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Category> CreateAsync(Category entity)
    {
        if (entity == null) { throw new ArgumentNullException(nameof(entity)); }

        await _dbContext.Categories.AddAsync(entity);
        await _dbContext.SaveChangesAsync();

        return entity;
    }

    public async Task<IReadOnlyCollection<Category>> GetAllAsync() => await _dbContext.Categories.ToListAsync();

    public async Task<IReadOnlyCollection<Category>> GetAllAsync(Expression<Func<Category, bool>> filter) => await _dbContext.Categories.Where(filter).ToListAsync();

    [return: MaybeNull]
    public async Task<Category> GetAsync(Expression<Func<Category, bool>> filter) => await _dbContext.Categories.FirstOrDefaultAsync(filter);

    public async Task RemoveAsync(Category entity)
    {
        _dbContext.Categories.Remove(entity);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(Category entity)
    {
        if (entity == null) { throw new ArgumentNullException(nameof(entity)); }

        // Find the tracked entity in the context by its primary key
        var trackedEntity = await GetAsync(category => category.Equals(entity));

        if (trackedEntity == null)
        {
            throw new InvalidOperationException($"Category with ID {entity.Id} not found.");
        }

        // Update properties of the tracked entity with the new values
        _dbContext.Entry(trackedEntity).CurrentValues.SetValues(entity);

        await _dbContext.SaveChangesAsync();
    }
}
