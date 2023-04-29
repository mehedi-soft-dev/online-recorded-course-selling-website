using Mapster;
using Microsoft.AspNetCore.Identity;
using RecordedCourseSellingApp.DataAccess.Identity.Entities;
using RecordedCourseSellingApp.DataAccess.UnitOfWorks;
using RecordedCourseSellingApp.Services.BusinessObjects;
using RecordedCourseSellingApp.Services.DTOs;
using CartItemEO = RecordedCourseSellingApp.DataAccess.Entities.CartItem;
using EnrollmentEO = RecordedCourseSellingApp.DataAccess.Entities.Enrollment;

namespace RecordedCourseSellingApp.Services.Services;

public class EnrollmentService : IEnrollmentService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IUnitOfWork _unitOfWork;

    public EnrollmentService(UserManager<ApplicationUser> userManger,
        IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        _userManager = userManger;
    }

    public async Task AddCartItemAsync(CartItem item)
    {
        var user = await _userManager.FindByNameAsync(item.Username);
        if (user is null) throw new Exception("User not found");

        var course = await _unitOfWork.Courses.GetSingleAsync(item.CourseId);
        if (course is null) throw new Exception("Course not found");

        var entityCount = await _unitOfWork.Enrollments.GetCountAsync(x => x.Course == course && x.User == user);
        if (entityCount > 0) throw new Exception("Already enrolled in this course");

        var cartItem = item.Adapt<CartItemEO>();
        cartItem.User = user;
        cartItem.Course = course;
        cartItem.Price = course.Price;

        var count = await _unitOfWork.CartItems.GetCountAsync(x => x.User == user && x.Course == course);

        if (count > 0) throw new Exception("Cart item already added");

        await _unitOfWork.BeginTransaction();
        await _unitOfWork.CartItems.AddAsync(cartItem);
        await _unitOfWork.Commit();
    }

    public async Task CreateCheckoutAsync(string username)
    {
        var user = await _userManager.FindByNameAsync(username);
        if (user == null)
            throw new Exception("User not found");

        var cartItems = await _unitOfWork.CartItems.FindAsync(x => x.User == user);
        if (cartItems.Count() == 0)
            throw new Exception("No Cart item found with this user");

        await _unitOfWork.BeginTransaction();

        foreach (var cartItem in cartItems)
        {
            var enrollment = new EnrollmentEO()
            {
                User = user,
                Course = cartItem.Course,
                Price = cartItem.Price,
            };

            await _unitOfWork.Enrollments.AddAsync(enrollment);
            await _unitOfWork.CartItems.DeleteAsync(cartItem);
        }

        await _unitOfWork.Commit();
    }

    public async Task<IEnumerable<CartItem>> GetCartItemsAsync(string username)
    {
        var user = await _userManager.FindByNameAsync(username);

        if (user is null) throw new Exception("User not found");

        var entities = await _unitOfWork.CartItems.FindAsync(x => x.User == user);

        IList<CartItem> cartItems = new List<CartItem>();

        foreach (var entity in entities)
        {
            cartItems.Add(entity.Adapt<CartItem>());
        }

        return cartItems;
    }

    public async Task<(int CartItems, int TotalAmount)> GetCheckoutDataAsync(string username)
    {
        var user = await _userManager.FindByNameAsync(username);
        if (user is null) throw new Exception("User not found");

        var cartItems = await _unitOfWork.CartItems.FindAsync(x => x.User == user);

        if (cartItems is null)
            return (0, 0);

        return (cartItems.Count(), cartItems.Sum(x => x.Price));
    }

    public async Task RemoveCartItemAsync(Guid id)
    {
        var cartItem = await _unitOfWork.CartItems.GetSingleAsync(x => x.Id == id);

        if (cartItem == null)
            throw new Exception("Cart item not found");


        await _unitOfWork.BeginTransaction();
        await _unitOfWork.CartItems.DeleteAsync(cartItem!);
        await _unitOfWork.Commit();
    }

    public async Task<IEnumerable<CourseListDto>> GetEnrolledCoursesAsync(string username)
    {
        var user = await _userManager.FindByNameAsync(username);
        if (user is null) throw new Exception("User not found");

        var enrolledCourses = await _unitOfWork.Enrollments.FindAsync(x => x.User == user);

        var list = new List<CourseListDto>();

        foreach (var course in enrolledCourses)
        {
            list.Add(course.Adapt<CourseListDto>());
        }

        return list;
    }

    public async Task<EnrolledCourseDto> GetEnrolledCourseDetailsAsync(string username, Guid courseId)
    {
        var user = await _userManager.FindByNameAsync(username);
        if (user is null) throw new Exception("User not found");

        var course = await _unitOfWork.Courses.GetSingleAsync(courseId);
        if (course == null) throw new Exception("Course not found");

        var isEnrolled = await _unitOfWork.Enrollments.GetCountAsync(x => x.Course == course && x.User == user);
        if (isEnrolled == 0) throw new Exception("Course is not enrolled");

        return course.Adapt<EnrolledCourseDto>();
    }
}
