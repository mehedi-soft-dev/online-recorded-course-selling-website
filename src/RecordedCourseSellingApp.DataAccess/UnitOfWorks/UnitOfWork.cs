using NHibernate;
using RecordedCourseSellingApp.DataAccess.Repositories;

namespace RecordedCourseSellingApp.DataAccess.UnitOfWorks;

public class UnitOfWork : IUnitOfWork
{
    private readonly ISession _session;
    private readonly ITransaction _transaction;
    private ICategoryRepository _categoryRepository;

    public UnitOfWork(ISession session,
        ICategoryRepository categoryRepository)
    {
        _session = session;
        _transaction = _session.BeginTransaction();
        _categoryRepository = categoryRepository;
    }

    public async Task BeginTransaction()
    {
        await Task.Run(() => _transaction.Begin());
    }

    public async Task Commit()
    {
        await Task.Run(() => _transaction.Commit());
    }

    public async Task Rollback()
    {
        await Task.Run(() => _transaction.Rollback());
    }

    public void Dispose()
    {
        _transaction.Dispose();
        _session.Dispose();
    }

    public ICategoryRepository Categories => _categoryRepository;
}
