using RecordedCourseSellingApp.Shared.Enums;

namespace RecordedCourseSellingApp.Services.DTOs;

public class CourseListDto
{
    public Guid CourseId { get; set; }

    public string Title { get; set; } = string.Empty;

    public Guid CategoryId { get; set; }

    public string CategoryName { get; set; } = string.Empty;

    public DifficultyLevel DifficultyLevel { get; set; }

    public int Price { get; set; }

    public string? ThumbnailImage { get; set; }
}
