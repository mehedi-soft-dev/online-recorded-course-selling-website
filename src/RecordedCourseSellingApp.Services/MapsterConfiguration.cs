using Mapster;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using CourseEO = RecordedCourseSellingApp.DataAccess.Entities.Course;
using CourseBO = RecordedCourseSellingApp.Services.BusinessObjects.Course;

using CartItemEO = RecordedCourseSellingApp.DataAccess.Entities.CartItem;
using CartItemBO = RecordedCourseSellingApp.Services.BusinessObjects.CartItem;


using RecordedCourseSellingApp.Services.DTOs;

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

        TypeAdapterConfig<CourseEO, CourseListDto>
            .NewConfig()
            .Map(dest => dest.CourseId, src => src.Id)
            .Map(dest => dest.CategoryName, src => src.Category.Name);

        TypeAdapterConfig<CourseEO, CourseDetailsDto>
            .NewConfig()
            .Map(dest => dest.CourseId, src => src.Id)
            .Map(dest => dest.CategoryName, src => src.Category.Name);

        TypeAdapterConfig<CartItemEO, CartItemBO>
            .NewConfig()
            .Map(dest => dest.CourseId, src => src.Course.Id)
            .Map(dest => dest.Title, src => src.Course.Title)
            .Map(dest => dest.ThumbnailImage, src => src.Course.ThumbnailImage)
            .Map(dest => dest.Price, src => src.Course.Price);
    }
}
