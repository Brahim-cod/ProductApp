using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository;

public interface IRepository<T, ID>
{
    Task<T> CreateAsync(T entity);
    Task<IReadOnlyCollection<T>> GetAllAsync();
    Task<IReadOnlyCollection<T>> GetAllAsync(Expression<Func<T, bool>> filter);
    [return: MaybeNull]
    Task<T> GetAsync(Expression<Func<T, bool>> filter);
    Task RemoveAsync(T entity);
    Task UpdateAsync(T entity);
}
