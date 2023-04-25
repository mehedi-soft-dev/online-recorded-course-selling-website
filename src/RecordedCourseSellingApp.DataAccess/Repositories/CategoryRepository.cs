using NHibernate;
using RecordedCourseSellingApp.DataAccess.Entities;
namespace RecordedCourseSellingApp.DataAccess.Repositories;

public class CategoryRepository : Repository<Category, Guid>, ICategoryRepository
{
    public CategoryRepository(ISession session) : base(session)
    {

    }
}
