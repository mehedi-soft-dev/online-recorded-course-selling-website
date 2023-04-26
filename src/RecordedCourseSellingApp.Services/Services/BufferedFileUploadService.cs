using Microsoft.AspNetCore.Http;

namespace RecordedCourseSellingApp.Services.Services;

public class BufferedFileUploadService : IBufferedFileUploadService
{
    public async Task<string?> UploadFileAsync(IFormFile file)
    {
        string path = "";

        try
        {
            if (file.Length > 0)
            {
                path = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, "wwwroot/UploadedFiles/Course/Thumbnail"));
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                string extension = Path.GetExtension(file.FileName);
                string uniqueFileName = Guid.NewGuid().ToString() + extension;

                using (var fileStream = new FileStream(Path.Combine(path, uniqueFileName), FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }

                return uniqueFileName;
            }
            else
            {
                return null;
            }
        }
        catch (Exception ex)
        {
            throw new Exception("File Copy Failed", ex);
        }
    }
}
