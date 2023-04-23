using Microsoft.AspNetCore.Identity;

namespace RecordedCourseSellingApp.DataAccess.Membership.Entities;

public class ApplicationUserLogin : IdentityUserLogin<Guid>
{
    protected bool Equals(ApplicationUserLogin other) => 
        LoginProvider == other.LoginProvider && ProviderKey == other.ProviderKey;

    public override bool Equals(object? obj) 
    {
        if (ReferenceEquals(null, obj))
            return false;
        
        if (ReferenceEquals(this, obj)) 
            return true;
        
        if (obj.GetType() != GetType())
            return false;
        
        return Equals((ApplicationUserLogin)obj);
    }

    public override int GetHashCode() 
    {
        unchecked 
        {
            var hashCode = 0;
            hashCode = LoginProvider.GetHashCode();
            hashCode = (hashCode * 397) ^ ProviderKey.GetHashCode();
            return hashCode;
        }
    }
}
