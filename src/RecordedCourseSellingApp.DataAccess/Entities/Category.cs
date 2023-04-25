namespace RecordedCourseSellingApp.DataAccess.Entities;

public class Category : IEntity<Guid>
{
    public virtual Guid Id { get; set; }

    public virtual string Name { get; set; } = string.Empty;

    public virtual string? Description { get; set; }
}
