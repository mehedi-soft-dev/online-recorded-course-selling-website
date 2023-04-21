namespace RecordedCourseSellingApp.DataAccess.Entities;

public class Customer
{
    public virtual Guid Id { get; set; }
    
    public virtual string Name { get; set; } = string.Empty;
    
    public virtual string Email { get; set; } = string.Empty;
}
