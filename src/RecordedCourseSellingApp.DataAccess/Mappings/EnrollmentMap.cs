using NHibernate;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using RecordedCourseSellingApp.DataAccess.Entities;
using RecordedCourseSellingApp.DataAccess.Identity.Entities;

namespace RecordedCourseSellingApp.DataAccess.Mappings;

public class EnrollmentMap : ClassMapping<Enrollment>
{
    public EnrollmentMap()
    {
        Schema("dbo");
        Table("Enrollments");
        Id(x => x.Id, x =>
        {
            x.Generator(Generators.Guid);
            x.Type(NHibernateUtil.Guid);
            x.Column("Id");
            x.UnsavedValue(Guid.Empty);
        });
        ManyToOne(x => x.User, m =>
        {
            m.Column("UserId");
            m.Class(typeof(ApplicationUser));
            m.Cascade(Cascade.Persist);
            m.NotNullable(true);
        });
        ManyToOne(x => x.Course, m =>
        {
            m.Column("CourseId");
            m.Class(typeof(Course));
            m.Cascade(Cascade.Persist);
            m.NotNullable(true);
        });
        Property(b => b.Price, x =>
        {
            x.Type(NHibernateUtil.Int32);
            x.NotNullable(true);
        });
    }
}
