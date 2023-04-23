using NHibernate;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using RecordedCourseSellingApp.DataAccess.Identity.Entities;

namespace RecordedCourseSellingApp.Web.Entities;

public class AppUserMap : JoinedSubclassMapping<AppUser>
{
    public AppUserMap()
    {
        Extends(typeof(ApplicationUser));
        ExplicitDeclarationsHolder.AddAsRootEntity(typeof(ApplicationUser));
        
        Schema("public");
        Table("app_users");
        Key(k => k.Column("id"));
        Property(
            e => e.CreateTime,
            mapping => {
                mapping.Column("create_time");
                mapping.Type(NHibernateUtil.DateTime);
                mapping.NotNullable(true);
                mapping.Generated(PropertyGeneration.Insert);
                mapping.Update(false);
                mapping.Insert(false);
            }
        );
        Property(
            e => e.LastLogin,
            mapping => {
                mapping.Column("last_login");
                mapping.Type(NHibernateUtil.DateTime);
                mapping.NotNullable(false);
            }
        );
        Property(
            e => e.LoginCount,
            mapping => {
                mapping.Column("login_count");
                mapping.Type(NHibernateUtil.Int32);
                mapping.NotNullable(true);
            }
        );
    }    
}
