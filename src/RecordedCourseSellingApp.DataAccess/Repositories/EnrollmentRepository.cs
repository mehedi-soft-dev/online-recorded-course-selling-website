using NHibernate;
using RecordedCourseSellingApp.DataAccess.Entities;

namespace RecordedCourseSellingApp.DataAccess.Repositories;

public class EnrollmentRepository : Repository<Enrollment, Guid>, IEnrollmentRepository
{
    public EnrollmentRepository(ISession session) : base(session)
    {

    }
}
