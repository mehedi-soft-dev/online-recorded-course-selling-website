using System.Linq.Expressions;

namespace RecordedCourseSellingApp.DataAccess.Repositories;

public interface IRepository<TEntity> where TEntity : class
{
    void Add(TEntity entity);
    
    void Update(TEntity entity);
    
    void Delete(TEntity entity);
    
    TEntity GetById(int id);
    
    IEnumerable<TEntity> GetAll();
}
