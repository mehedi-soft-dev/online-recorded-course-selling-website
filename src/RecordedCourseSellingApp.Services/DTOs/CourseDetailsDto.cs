using RecordedCourseSellingApp.Shared.Enums;

namespace RecordedCourseSellingApp.Services.DTOs;

public class CourseDetailsDto
{
    public Guid CourseId { get; set; }

    public string Title { get; set; } = string.Empty;

    public string? Description { get; set; }

    public string CategoryName { get; set; } = string.Empty;

    public int Price { get; set; }

    public DifficultyLevel DifficultyLevel { get; set; }

    public string? ThumbnailImage { get; set; }

    public string VideoUrl { get; set; } = string.Empty;

    public bool AlreadyAddedToCart { get; set; }

    public bool AlreadyEnrolled { get; set; }
}
