using RecordedCourseSellingApp.Shared.Enums;

namespace RecordedCourseSellingApp.DataAccess.Entities;

public class Course : IEntity<Guid>
{
    public virtual Guid Id { get; set; }

    public virtual string Title { get; set; } = string.Empty;

    public virtual DifficultyLevel DifficultyLevel { get; set; }

    public virtual string? ThumbnailImage { get; set; }

    public virtual string VideoUrl { get; set; } = string.Empty;

    public virtual string? Description { get; set; }

    public virtual int Price { get; set; }

    public virtual Category Category { get; set; }
}
