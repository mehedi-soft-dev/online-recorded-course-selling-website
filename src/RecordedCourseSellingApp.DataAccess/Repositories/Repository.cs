using NHibernate;

namespace RecordedCourseSellingApp.DataAccess.Repositories;

public class Repository<TEntity> : IRepository<TEntity> 
    where TEntity : class 
{
    private readonly ISession _session;

    public Repository(ISession session)
    {
        _session = session;
    }

    public void Add(TEntity entity)
    {
        _session.Save(entity);
    }

    public void Update(TEntity entity)
    {
        _session.Update(entity);
    }

    public void Delete(TEntity entity)
    {
        _session.Delete(entity);
    }

    public TEntity GetById(int id)
    {
        return _session.Get<TEntity>(id);
    }

    public IEnumerable<TEntity> GetAll()
    {
        return _session.Query<TEntity>();
    }
}
