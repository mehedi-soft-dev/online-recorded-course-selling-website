using RecordedCourseSellingApp.DataAccess.Repositories;

namespace RecordedCourseSellingApp.DataAccess.UnitOfWorks;

public interface IUnitOfWork : IDisposable
{
    void BeginTransaction();
    
    void Commit();
    
    void Rollback();

    ICustomerRepository Customers { get; }
    ICategoryRepository Categories { get; }
}
