using NHibernate.Mapping.Attributes;
using RecordedCourseSellingApp.DataAccess.Identity.Entities;

namespace RecordedCourseSellingApp.Web.Entities;

[JoinedSubclass(0, Schema = "public", Table = "app_roles", ExtendsType = typeof(ApplicationRole))]
[Key(1, Column = "id")]
public class AppRole : ApplicationRole 
{

    [Property(Column = "description", Type = "string", Length = 256, NotNull = false)]
    public virtual string? Description { get; set; }
}
