using Microsoft.AspNetCore.Http;

namespace RecordedCourseSellingApp.Services.Services;

public interface IBufferedFileUploadService
{
    Task<string?> UploadFileAsync(IFormFile file);
}
