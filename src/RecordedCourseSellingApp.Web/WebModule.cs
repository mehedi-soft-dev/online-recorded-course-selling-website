using Autofac;
using RecordedCourseSellingApp.Web.Models;

namespace RecordedCourseSellingApp.Web;

public class WebModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<SignUpModel>().AsSelf();
        
        base.Load(builder);
    }
}
