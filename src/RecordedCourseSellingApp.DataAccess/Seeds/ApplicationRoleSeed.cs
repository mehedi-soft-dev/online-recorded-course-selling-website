using RecordedCourseSellingApp.DataAccess.Identity.Entities;

namespace RecordedCourseSellingApp.DataAccess.Seeds;

public static class ApplicationRoleSeed
{
    public static ApplicationRole[] GetRoles
    {
        get
        {
            return new ApplicationRole[]
            {
                new ApplicationRole
                {
                    Id = Guid.Parse("EE94C1AD-2ABA-453A-84EB-A6539E9FD5A6"),
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                },
                new ApplicationRole
                {
                    Id = Guid.Parse("41469738-A049-405C-A0A6-A52CA0AE4C38"),
                    Name = "User",
                    NormalizedName = "USER"
                }
            };
        }
    }
}
