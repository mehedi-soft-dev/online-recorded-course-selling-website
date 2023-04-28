using RecordedCourseSellingApp.DataAccess.Entities;

namespace RecordedCourseSellingApp.DataAccess.Repositories;

public interface ICartRepository : IRepository<CartItem, Guid>
{

}
