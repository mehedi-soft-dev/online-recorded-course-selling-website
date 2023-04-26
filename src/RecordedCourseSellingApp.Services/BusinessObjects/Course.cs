using RecordedCourseSellingApp.Shared.Enums;

namespace RecordedCourseSellingApp.Services.BusinessObjects;

public class Course
{
    public Guid Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public DifficultyLevel DifficultyLevel { get; set; }

    public string? Description { get; set; }

    public decimal Price { get; set; }

}
