namespace RecordedCourseSellingApp.Services.BusinessObjects;

public class CartItem
{
    public Guid CartItemId { get; set; }

    public Guid UserId { get; set; }

    public string Username { get; set; }

    public Guid CourseId { get; set; }

    public int Price { get; set; }
}
