using RecordedCourseSellingApp.DataAccess.Repositories;

namespace RecordedCourseSellingApp.DataAccess.UnitOfWorks;

public interface IUnitOfWork : IDisposable
{
    Task BeginTransaction();
    
    Task Commit();
    
    Task Rollback();

    ICategoryRepository Categories { get; }
    ICourseRepository Courses { get; }
    ICartRepository Carts { get; }
}
