using NHibernate;
using NHibernate.Mapping.ByCode.Conformist;
using RecordedCourseSellingApp.DataAccess.Identity.Entities;

namespace RecordedCourseSellingApp.Web.Entities;

public class AppRoleMap : JoinedSubclassMapping<AppRole>
{
    public AppRoleMap()
    {
        ExplicitDeclarationsHolder.AddAsRootEntity(typeof(ApplicationRole));
        Extends(typeof(ApplicationRole));
        
        Schema("public");
        Table("app_roles");
        Key(k => k.Column("id"));
        Property(
            p => p.Description,
            mapping => {
                mapping.Column("description");
                mapping.Type(NHibernateUtil.String);
                mapping.Length(256);
            }
        );
    }
}
