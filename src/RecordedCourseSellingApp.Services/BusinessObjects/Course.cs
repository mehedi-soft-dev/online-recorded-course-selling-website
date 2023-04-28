using RecordedCourseSellingApp.Shared.Enums;

namespace RecordedCourseSellingApp.Services.BusinessObjects;

public class Course
{
    public Guid Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public DifficultyLevel DifficultyLevel { get; set; }

    public string? ThumbnailImage { get; set; }

    public string VideoUrl { get; set; } = string.Empty;

    public string? Description { get; set; }

    public int Price { get; set; }

    public Guid CategoryId { get; set; }

    public string Categoryname { get; set; } = string.Empty;
}
