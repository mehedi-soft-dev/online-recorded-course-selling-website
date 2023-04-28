using Autofac;
using RecordedCourseSellingApp.Web.Areas.Admin.Models;
using RecordedCourseSellingApp.Web.Areas.Identity.Models;
using RecordedCourseSellingApp.Web.Models;

namespace RecordedCourseSellingApp.Web;

public class WebModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<DashboardModel>().AsSelf();

        builder.RegisterType<SignUpModel>().AsSelf();
        builder.RegisterType<SignInModel>().AsSelf();

        builder.RegisterType<CategoryCreateModel>().AsSelf();
        builder.RegisterType<CategoryListModel>().AsSelf();
        builder.RegisterType<CategoryEditModel>().AsSelf();

        builder.RegisterType<CourseCreateModel>().AsSelf();
        builder.RegisterType<CourseListModel>().AsSelf();
        builder.RegisterType<CourseEditModel>().AsSelf();

        builder.RegisterType<CourseSearchModel>().AsSelf();

        base.Load(builder);
    }
}
