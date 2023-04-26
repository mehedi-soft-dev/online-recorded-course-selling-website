using NHibernate;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using NHibernate.Type;
using RecordedCourseSellingApp.DataAccess.Entities;
using RecordedCourseSellingApp.Shared.Enums;

namespace RecordedCourseSellingApp.DataAccess.Mappings;

public class CourseMap : ClassMapping<Course>
{
    public CourseMap()
    {
        Schema("dbo");
        Table("Courses");
        Id(x => x.Id, x =>
        {
            x.Generator(Generators.Guid);
            x.Type(NHibernateUtil.Guid);
            x.Column("Id");
            x.UnsavedValue(Guid.Empty);
        });
        Property(b => b.Title, x =>
        {
            x.Length(50);
            x.Type(NHibernateUtil.StringClob);
            x.NotNullable(true);
        });
        Property(b => b.DifficultyLevel, x =>
        {
            x.Type<EnumType<DifficultyLevel>>();
            x.NotNullable(true);
        });
        Property(b => b.Description, x =>
        {
            x.Length(256);
            x.Type(NHibernateUtil.StringClob);
        });
        Property(b => b.Price, x =>
        {
            x.Length(5000);
            x.Type(NHibernateUtil.Decimal);
        });
        ManyToOne(x => x.Category, m =>
        {
            m.Column("CategoryId");
            m.Class(typeof(Category));
            m.Cascade(Cascade.Persist);
        });
    }
}
