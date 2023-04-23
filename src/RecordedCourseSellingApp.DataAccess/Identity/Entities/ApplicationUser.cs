using Microsoft.AspNetCore.Identity;

namespace RecordedCourseSellingApp.DataAccess.Identity.Entities;

public class ApplicationUser : IdentityUser<Guid>
{
    public virtual string FirstName { get; set; }
    
    public virtual string LastName { get; set; }
}
