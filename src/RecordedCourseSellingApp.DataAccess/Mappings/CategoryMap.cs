using NHibernate;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using RecordedCourseSellingApp.DataAccess.Entities;

namespace RecordedCourseSellingApp.DataAccess.Mappings;

public class CategoryMap : ClassMapping<Category>
{
    public CategoryMap()
    {
        Schema("dbo");
        Table("ApplicationRoleClaims");
        Id(x => x.Id, x =>
        {
            x.Generator(Generators.Guid);
            x.Type(NHibernateUtil.Guid);
            x.Column("Id");
            x.UnsavedValue(Guid.Empty);
        });
        Property(b => b.Name, x =>
        {
            x.Length(50);
            x.Type(NHibernateUtil.StringClob);
            x.NotNullable(true);
        });
        Property(b => b.Description, x =>
        {
            x.Length(256);
            x.Type(NHibernateUtil.StringClob);
        });
    }
}
