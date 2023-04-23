using NHibernate;
using NHibernate.Mapping.ByCode.Conformist;
using RecordedCourseSellingApp.DataAccess.Identity.Entities;

namespace RecordedCourseSellingApp.DataAccess.Identity.Mappings;

public class ApplicationUserTokenMap : ClassMapping<ApplicationUserToken>
{
    public ApplicationUserTokenMap()
    {
        Schema("dbo");
        Table("ApplicationUserTokens");
        ComposedId(id => {
            id.Property(e => e.UserId, prop => {
                prop.Column("UserId");
                prop.Type(NHibernateUtil.Guid);
            });
            id.Property(e => e.LoginProvider, prop => {
                prop.Column("LoginProvider");
                prop.Type(NHibernateUtil.String);
                prop.Length(32);
            });
            id.Property(e => e.Name, prop => {
                prop.Column("Name");
                prop.Type(NHibernateUtil.String);
                prop.Length(32);
            });
        });
        Property(e => e.Value, prop => {
            prop.Column("Value");
            prop.Type(NHibernateUtil.String);
            prop.Length(256);
            prop.NotNullable(true);
        });
    }
}
