using RecordedCourseSellingApp.DataAccess.Repositories;

namespace RecordedCourseSellingApp.DataAccess.UnitOfWorks;

public interface IUnitOfWork : IDisposable
{
    Task BeginTransaction();
    
    Task Commit();
    
    Task Rollback();

    ICustomerRepository Customers { get; }
    ICategoryRepository Categories { get; }
}
