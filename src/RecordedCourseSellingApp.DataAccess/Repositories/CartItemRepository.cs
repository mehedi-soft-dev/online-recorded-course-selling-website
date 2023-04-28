using NHibernate;
using RecordedCourseSellingApp.DataAccess.Entities;

namespace RecordedCourseSellingApp.DataAccess.Repositories;

public class CartItemRepository : Repository<CartItem, Guid>, ICartItemRepository
{
    public CartItemRepository(ISession session) : base(session)
    {

    }
}
