using NHibernate;
using RecordedCourseSellingApp.DataAccess.Entities;

namespace RecordedCourseSellingApp.DataAccess.Repositories;

public class CustomerRepository : Repository<Customer, Guid>, ICustomerRepository
{
    public CustomerRepository(ISession session) : base(session)
    {
        
    }
}
