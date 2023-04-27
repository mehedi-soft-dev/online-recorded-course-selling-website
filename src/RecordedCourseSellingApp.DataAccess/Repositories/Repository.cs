using NHibernate;
using NHibernate.Linq;
using RecordedCourseSellingApp.DataAccess.Entities;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;

namespace RecordedCourseSellingApp.DataAccess.Repositories;

public class Repository<T, TKey> : IRepository<T, TKey>
    where T : class, IEntity<TKey>
{
    private readonly ISession _session;

    public Repository(ISession session)
    {
        _session = session;
    }

    public async Task AddAsync(T entity) => await _session.SaveAsync(entity);
    
    public async Task UpdateAsync(T entity) => await _session.SaveOrUpdateAsync(entity);
    
    public async Task DeleteAsync(T entity) => await _session.DeleteAsync(entity);
    
    public async Task AddOrUpdateAsync(T entity) => await _session.SaveOrUpdateAsync(entity);
    
    public async Task MergeAsync(T entity) => await _session.MergeAsync(entity);
    
    public async Task<T?> GetSingleAsync(TKey id) => await _session.GetAsync<T>(id);

    public async Task<T?> GetSingleAsync(Expression<Func<T, bool>> predicate) => 
        await _session.QueryOver<T>().Where(predicate).SingleOrDefaultAsync();
    
    public async Task<IEnumerable<T>> GetAllAsync() => 
        await Task.Run(() =>_session.Query<T>().ToList());
    
    public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>>? predicate = null!)
    {
        var query = _session.Query<T>();

        if (predicate != null)
            query = query.Where(predicate);

        return await query.ToListAsync();
    }

    public virtual async Task<(IList<T> data, int total, int totalDisplay)> 
        GetByPagingAsync(Expression<Func<T, bool>> filter = null!, string orderBy = null!, 
            int pageIndex = 1, int pageSize = 10, Expression<Func<T, object>>? objectSelector = null!,
			Expression<Func<T, bool>> selectorFilter = null!)
    {
        IQueryable<T> query = _session.Query<T>();
        var total = query.Count();
        var totalDisplay = query.Count();

        if (filter != null)
        {
            query = query.Where(filter);
            totalDisplay = query.Count();
        }
        
        if (objectSelector != null)
        {
            query = query.Fetch(objectSelector);

            if (selectorFilter != null)
                query = query.Where(selectorFilter);
		}

        if (orderBy != null)
        {
            var result = query.OrderBy(orderBy).Skip((pageIndex - 1) * pageSize).Take(pageSize);
            return (await result.ToListAsync(), total, totalDisplay);
        }
        else
        {
            var result = query.Skip((pageIndex - 1) * pageSize).Take(pageSize);
            return (await result.ToListAsync(), total, totalDisplay);
        }
    }
}
