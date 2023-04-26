using RecordedCourseSellingApp.DataAccess.Identity.Entities;
using RecordedCourseSellingApp.DataAccess.Identity.Extensions;
using RecordedCourseSellingApp.DataAccess.Repositories;
using RecordedCourseSellingApp.DataAccess.UnitOfWorks;

namespace RecordedCourseSellingApp.DataAccess;
using Microsoft.Extensions.DependencyInjection;

public static class ConfigureServices
{
    public static IServiceCollection AddDataAccessLayer(this IServiceCollection services, 
        string connectionString)
    {
        //Configure NHibernate
        services.AddNHibernate(connectionString);
        
        //Configure Identity
        services.AddDefaultIdentity<ApplicationUser>()
            .AddRoles<ApplicationRole>()
            .AddHibernateStores();
        
        //Register Unit of work
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        
        //Register Repositories
        services.AddScoped<ICategoryRepository, CategoryRepository>();

        return services;
    }
}
