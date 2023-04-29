using RecordedCourseSellingApp.Services.BusinessObjects;
using RecordedCourseSellingApp.Services.DTOs;

namespace RecordedCourseSellingApp.Services.Services;

public interface IEnrollmentService
{
    Task AddCartItemAsync(CartItem item);

    Task RemoveCartItemAsync(Guid id);

    Task<IEnumerable<CartItem>> GetCartItemsAsync(string username);

    Task CreateCheckoutAsync(string username);

    Task<(int CartItems, int TotalAmount)> GetCheckoutDataAsync(string username);

    Task<IEnumerable<CourseListDto>> GetEnrolledCoursesAsync(string username);

    Task<EnrolledCourseDto> GetEnrolledCourseDetailsAsync(string username, Guid courseId);
}
