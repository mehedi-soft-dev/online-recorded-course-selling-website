namespace RecordedCourseSellingApp.Services.DTOs;

public sealed class CustomerDto
{
    public Guid Id { get; set; }
    
    public string Name { get; set; } = string.Empty;
    
    public string Email { get; set; } = string.Empty;
}
