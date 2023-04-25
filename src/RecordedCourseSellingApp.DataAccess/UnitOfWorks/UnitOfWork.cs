using NHibernate;
using RecordedCourseSellingApp.DataAccess.Repositories;

namespace RecordedCourseSellingApp.DataAccess.UnitOfWorks;

public class UnitOfWork : IUnitOfWork
{
    private readonly ISession _session;
    private readonly ITransaction _transaction;
    private ICustomerRepository _customerRepository;
    private ICategoryRepository _categoryRepository;

    public UnitOfWork(ISession session, ICustomerRepository customerRepository, 
        ICategoryRepository categoryRepository)
    {
        _session = session;
        _transaction = _session.BeginTransaction();
        _customerRepository = customerRepository;
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

    public ICustomerRepository Customers => _customerRepository;
    public ICategoryRepository Categories => _categoryRepository;
}
