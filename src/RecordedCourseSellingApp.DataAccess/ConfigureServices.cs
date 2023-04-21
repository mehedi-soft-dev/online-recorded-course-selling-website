using RecordedCourseSellingApp.DataAccess.Repositories;
using RecordedCourseSellingApp.DataAccess.UnitOfWorks;

namespace RecordedCourseSellingApp.DataAccess;
using Microsoft.Extensions.DependencyInjection;

public static class ConfigureServices
{
    public static IServiceCollection AddDataAccess(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        
        //Repositories
        services.AddScoped<ICustomerRepository, CustomerRepository>();

        return services;
    }
}
