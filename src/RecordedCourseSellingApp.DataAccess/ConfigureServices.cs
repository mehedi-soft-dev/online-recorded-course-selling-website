using RecordedCourseSellingApp.DataAccess.Repositories;
using RecordedCourseSellingApp.DataAccess.UnitOfWorks;

namespace RecordedCourseSellingApp.DataAccess;
using Microsoft.Extensions.DependencyInjection;

public static class ConfigureServices
{
    public static IServiceCollection AddDataAccessLayer(this IServiceCollection services, 
        string connectionString)
    {
        services.AddNHibernate(connectionString);
        //Register Unit of work
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        
        //
        //Register Repositories
        services.AddScoped<ICustomerRepository, CustomerRepository>();

        return services;
    }
}
