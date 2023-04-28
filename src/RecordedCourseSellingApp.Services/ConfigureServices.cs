using Microsoft.Extensions.DependencyInjection;
using RecordedCourseSellingApp.Services.Services;

namespace RecordedCourseSellingApp.Services;

public static class ConfigureServices
{
    public static IServiceCollection AddServiceLayer(this IServiceCollection services)
    {
        //Register Mapster
        services.AddMapster();

        //Register BufferedFileUploadService
        services.AddScoped<IBufferedFileUploadService, BufferedFileUploadService>();

        //Service
        services.AddScoped<ISeedingService, SeedingService>();
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<ICourseService, CourseService>();
        services.AddScoped<IEnrollmentService, EnrollmentService>();
        
        return services;
    }
}
