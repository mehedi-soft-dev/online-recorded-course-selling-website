using RecordedCourseSellingApp.Services.BusinessObjects;
using RecordedCourseSellingApp.Services.DTOs;
using RecordedCourseSellingApp.Shared.Enums;

namespace RecordedCourseSellingApp.Services.Services;

public interface ICourseService
{
    Task CreateCourseAsync(Course course);

    Task<(int total, int totalDisplay, IList<Course> records)> GetCoursesByPagingAsync(int pageIndex,
        int pageSize, string searchText, string orderby);

    Task DeleteCourseAsync(Guid id);

    Task<Course?> GetCourseByIdAsync(Guid id);

    Task EditCourseAsync(Course course);

    Task<IList<Course>> GetAllCoursesAsync();

    Task<IList<CourseDto>> GetCoursesBySearchAsync(Guid? CategoryId = null, 
        DifficultyLevel? difficultyLevel = null, string? searchText = null);
}
