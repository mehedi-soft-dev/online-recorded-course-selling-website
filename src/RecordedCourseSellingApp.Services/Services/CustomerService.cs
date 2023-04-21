using RecordedCourseSellingApp.DataAccess.Entities;
using RecordedCourseSellingApp.DataAccess.UnitOfWorks;
using RecordedCourseSellingApp.Services.DTOs;

namespace RecordedCourseSellingApp.Services.Services;

public class CustomerService : ICustomerService
{
    private readonly IUnitOfWork _unitOfWork;

    public CustomerService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    public void Insert(CustomerDto customerDto)
    {
        var customer = new Customer()
        {
            Id = new Guid(),
            Email = customerDto.Email,
            Name = customerDto.Name,
        };
        _unitOfWork.Customers.AddAsync(customer);
        _unitOfWork.Commit();
    }
}
