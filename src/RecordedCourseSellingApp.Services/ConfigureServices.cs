using Microsoft.Extensions.DependencyInjection;
using RecordedCourseSellingApp.Services.Services;

namespace RecordedCourseSellingApp.Services;

public static class ConfigureServices
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        //Service
        services.AddScoped<ICustomerService, CustomerService>();
        
        return services;
    }
}