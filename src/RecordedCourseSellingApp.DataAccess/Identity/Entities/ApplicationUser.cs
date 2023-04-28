using Microsoft.AspNetCore.Identity;
using RecordedCourseSellingApp.DataAccess.Entities;

namespace RecordedCourseSellingApp.DataAccess.Identity.Entities;

public class ApplicationUser : IdentityUser<Guid>
{
    public virtual string FirstName { get; set; }
    
    public virtual string LastName { get; set; }
    
    public virtual long? LockoutEndUnixTimeSeconds { get; set; }

    public virtual IList<CartItem> CartItems { get; set; } 

    public override DateTimeOffset? LockoutEnd {
        get {
            if (!LockoutEndUnixTimeSeconds.HasValue) {
                return null;
            }
            var offset = DateTimeOffset.FromUnixTimeSeconds(
                LockoutEndUnixTimeSeconds.Value
            );
            return TimeZoneInfo.ConvertTime(offset, TimeZoneInfo.Local);
        }
        set {
            if (value.HasValue) {
                LockoutEndUnixTimeSeconds = value.Value.ToUnixTimeSeconds();
            }
            else {
                LockoutEndUnixTimeSeconds = null;
            }
        }
    }
}
