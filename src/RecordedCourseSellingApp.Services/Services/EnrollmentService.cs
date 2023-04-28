using Mapster;
using Microsoft.AspNetCore.Identity;
using RecordedCourseSellingApp.DataAccess.Identity.Entities;
using RecordedCourseSellingApp.DataAccess.UnitOfWorks;
using RecordedCourseSellingApp.Services.BusinessObjects;
using CartItemEO = RecordedCourseSellingApp.DataAccess.Entities.CartItem;

namespace RecordedCourseSellingApp.Services.Services;

public class EnrollmentService : IEnrollmentService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly UserManager<ApplicationUser> _userManager;

    public EnrollmentService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task AddCartItemAsync(CartItem item)
    {
        var user = await _userManager.FindByNameAsync(item.Username);
        var course = await _unitOfWork.Courses.GetSingleAsync(item.CourseId);

        if (user is null)
            throw new Exception("User not found");

        if (course is null)
            throw new Exception("Course not found");

        var cartItem = item.Adapt<CartItemEO>();
        cartItem.User = user;
        cartItem.Course = course;

        await _unitOfWork.BeginTransaction();
        await _unitOfWork.CartItems.AddAsync(cartItem);
        await _unitOfWork.Commit();
    }

    public async Task RemoveCartItemAsync(CartItem item)
    {
        throw new NotImplementedException();
    }
}
