using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Extensions;

public static class DbContextExtensions
{
    public static DbSet<TEntity> GetDbSet<TEntity>(this DbContext context) where TEntity : class
    {
        return context.Set<TEntity>();
    }

    public static async Task<int> GetNextIdentityValueAsync<TEntity>(this DbContext context) where TEntity : class
    {
        var entityType = context.Model.FindEntityType(typeof(TEntity));

        if (entityType == null)
            throw new InvalidOperationException($"Entity type {typeof(TEntity).Name} not found in the model.");

        var keyProperties = entityType.FindPrimaryKey().Properties.Select(p => p.Name).ToList();

        if (keyProperties.Count != 1)
            throw new InvalidOperationException($"Entity type {typeof(TEntity).Name} must have exactly one primary key property.");

        var propertyName = keyProperties.First();

        // Retrieve the next value by querying the database
        var sql = $"SELECT ISNULL(MAX([{propertyName}]) + 1, 1) FROM {entityType.GetTableName()}";
        var nextId = await context.Database.ExecuteSqlRawAsync(sql);

        return (int)nextId;
    }
}
