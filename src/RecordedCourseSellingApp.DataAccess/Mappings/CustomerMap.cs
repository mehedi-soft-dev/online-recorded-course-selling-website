using NHibernate;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using RecordedCourseSellingApp.DataAccess.Entities;

namespace RecordedCourseSellingApp.DataAccess.Mappings;

public class CustomerMap : ClassMapping<Customer>
{
    public CustomerMap()
    {
        Id(x => x.Id, x =>
        {
            x.Generator(Generators.Guid);
            x.Type(NHibernateUtil.Guid);
            x.Column("Id");
            x.UnsavedValue(Guid.Empty);
        });
 
        Property(b => b.Email, x =>
        {
            x.Length(50);
            x.Type(NHibernateUtil.StringClob);
            x.NotNullable(true);
        });
        
        Property(b => b.Name, x =>
        {
            x.Length(50);
            x.Type(NHibernateUtil.StringClob);
            x.NotNullable(true);
        });
 
        Table("Customers");
    }
}
