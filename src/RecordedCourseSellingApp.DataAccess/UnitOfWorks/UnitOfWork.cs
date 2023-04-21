using NHibernate;
using RecordedCourseSellingApp.DataAccess.Repositories;

namespace RecordedCourseSellingApp.DataAccess.UnitOfWorks;

public class UnitOfWork : IUnitOfWork
{
    private readonly ISession _session;
    private readonly ITransaction _transaction;
    private ICustomerRepository _customerRepository;

    public UnitOfWork(ISession session, ICustomerRepository customerRepository)
    {
        _session = session;
        _transaction = _session.BeginTransaction();
        _customerRepository = customerRepository;
    }

    public void BeginTransaction()
    {
        _transaction.Begin();
    }

    public void Commit()
    {
        _transaction.Commit();
    }

    public void Rollback()
    {
        _transaction.Rollback();
    }

    public void Dispose()
    {
        _transaction.Dispose();
        _session.Dispose();
    }

    public ICustomerRepository Customers => _customerRepository;
}
