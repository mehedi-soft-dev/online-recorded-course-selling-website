using Mapster;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using CourseEO = RecordedCourseSellingApp.DataAccess.Entities.Course;
using CourseBO = RecordedCourseSellingApp.Services.BusinessObjects.Course;

namespace RecordedCourseSellingApp.Services;

public static class MapsterConfiguration
{
    public static void AddMapster(this IServiceCollection services)
    {
        TypeAdapterConfig.GlobalSettings.Scan(Assembly.GetExecutingAssembly());

        TypeAdapterConfig<CourseEO, CourseBO>
            .NewConfig()
            .Map(dest => dest.CategoryId, src => src.Category.Id)
            .Map(dest => dest.Categoryname, src => src.Category.Name);
    }
}
