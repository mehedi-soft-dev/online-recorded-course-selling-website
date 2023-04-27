using System.Linq.Expressions;
using RecordedCourseSellingApp.DataAccess.Entities;

namespace RecordedCourseSellingApp.DataAccess.Repositories;

public interface IRepository<T, in TKey> 
    where T : class, IEntity<TKey>
{
    Task AddAsync(T entity);

    Task UpdateAsync(T entity);

    Task DeleteAsync(T entity);

    Task AddOrUpdateAsync(T entity);

    Task MergeAsync(T entity);
    
    Task<T?> GetSingleAsync(TKey id);

    Task<T?> GetSingleAsync(Expression<Func<T, bool>> predicate);
    
    Task<IEnumerable<T>> GetAllAsync();

    Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>>? predicate = null!);


	Task<(IList<T> data, int total, int totalDisplay)> GetByPagingAsync(
        Expression<Func<T, bool>> filter = null!, string orderBy = null!,int pageIndex = 1, 
        int pageSize = 10, Expression<Func<T, object>>? objectSelector = null!, 
        Expression<Func<T, bool>> selectorFilter = null!);
}
