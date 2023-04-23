using NHibernate;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using RecordedCourseSellingApp.DataAccess.Membership.Entities;

namespace RecordedCourseSellingApp.DataAccess.Membership.Mappings;

public class ApplicationUserClaimMap : ClassMapping<ApplicationUserClaim>
{
    public ApplicationUserClaimMap()
    {
        Schema("dbo");
        Table("ApplicationUserClaims");
        Id(e => e.Id, id => {
            id.Column("id");
            id.Type(NHibernateUtil.Int32);
            id.Generator(Generators.Identity);
        });
        Property(e => e.ClaimType, prop => {
            prop.Column("ClaimType");
            prop.Type(NHibernateUtil.String);
            prop.Length(1024);
            prop.NotNullable(true);
        });
        Property(e => e.ClaimValue, prop => {
            prop.Column("ClaimValue");
            prop.Type(NHibernateUtil.String);
            prop.Length(1024);
            prop.NotNullable(true);
        });
        Property(e => e.UserId, prop => {
            prop.Column("UserId");
            prop.Type(NHibernateUtil.String);
            prop.Length(32);
            prop.NotNullable(true);
        });
    }
}