using Mapster;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace RecordedCourseSellingApp.Services;

internal static class MapsterConfiguration
{
    internal static void AddMapster(this IServiceCollection services)
    {
        TypeAdapterConfig.GlobalSettings.Scan(Assembly.GetExecutingAssembly());
    }
}
