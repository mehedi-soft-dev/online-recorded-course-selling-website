using Autofac;
using RecordedCourseSellingApp.Web.Areas.Identity.Models;

namespace RecordedCourseSellingApp.Web;

public class WebModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<SignUpModel>().AsSelf();
        builder.RegisterType<SignInModel>().AsSelf();
        
        base.Load(builder);
    }
}