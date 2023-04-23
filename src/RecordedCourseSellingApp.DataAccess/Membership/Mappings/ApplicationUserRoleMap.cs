using NHibernate;
using NHibernate.Mapping.ByCode.Conformist;
using RecordedCourseSellingApp.DataAccess.Membership.Entities;

namespace RecordedCourseSellingApp.DataAccess.Membership.Mappings;

public class ApplicationUserRoleMap : ClassMapping<ApplicationUserRole>
{
    public ApplicationUserRoleMap()
    {
        Schema("dbo");
        Table("ApplicationUserRoles");
        ComposedId(id => {
            id.Property(e => e.UserId, prop => {
                prop.Column("UserId");
                prop.Type(NHibernateUtil.String);
                prop.Length(32);
            });
            id.Property(e => e.RoleId, prop => {
                prop.Column("RoleId");
                prop.Type(NHibernateUtil.String);
                prop.Length(32);
            });
        });
    }
}
