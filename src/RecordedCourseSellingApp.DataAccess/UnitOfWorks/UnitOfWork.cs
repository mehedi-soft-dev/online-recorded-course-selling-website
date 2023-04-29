using NHibernate;
using RecordedCourseSellingApp.DataAccess.Repositories;

namespace RecordedCourseSellingApp.DataAccess.UnitOfWorks;

public class UnitOfWork : IUnitOfWork
{
    private readonly ISession _session;
    private readonly ITransaction _transaction;
    private readonly ICategoryRepository _categoryRepository;
    private readonly ICourseRepository _courseRepository;
    private readonly ICartItemRepository _cartItemsRepository;
    private readonly IEnrollmentRepository _enrollmentRepository;

    public UnitOfWork(ISession session,
        ICategoryRepository categoryRepository,
        ICourseRepository courseRepository,
        ICartItemRepository cartItemsRepository,
        IEnrollmentRepository enrollmentRepository)
    {
        _session = session;
        _transaction = _session.BeginTransaction();
        _categoryRepository = categoryRepository;
        _courseRepository = courseRepository;
        _cartItemsRepository = cartItemsRepository;
        _enrollmentRepository = enrollmentRepository;
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
    public ICourseRepository Courses => _courseRepository;
    public ICartItemRepository CartItems => _cartItemsRepository;
    public IEnrollmentRepository Enrollments => _enrollmentRepository;
}
