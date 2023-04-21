using RecordedCourseSellingApp.Services.DTOs;

namespace RecordedCourseSellingApp.Services.Services;

public interface ICustomerService
{
    void Insert(CustomerDto customerDto);
}
