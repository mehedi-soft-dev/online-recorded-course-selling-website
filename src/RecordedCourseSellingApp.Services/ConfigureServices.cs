using Microsoft.Extensions.DependencyInjection;
using RecordedCourseSellingApp.Services.Services;

namespace RecordedCourseSellingApp.Services;

public static class ConfigureServices
{
    public static IServiceCollection AddServiceLayer(this IServiceCollection services)
    {
        //Register Mapster
        services.AddMapster();

        //Service
        services.AddScoped<ICustomerService, CustomerService>();
        services.AddScoped<ISeedingService, SeedingService>();
        services.AddScoped<ICategoryService, CategoryService>();
        
        return services;
    }
}
