using NHibernate;
using RecordedCourseSellingApp.DataAccess.Entities;

namespace RecordedCourseSellingApp.DataAccess.Repositories;

public class CartRepository : Repository<CartItem, Guid>, ICartRepository
{
    public CartRepository(ISession session) : base(session)
    {

    }
}
