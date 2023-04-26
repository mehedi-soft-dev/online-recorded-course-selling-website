using RecordedCourseSellingApp.Services.BusinessObjects;

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
}
