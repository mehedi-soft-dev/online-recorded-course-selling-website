using log4net;
using RecordedCourseSellingApp.DataAccess;
using RecordedCourseSellingApp.DataAccess.Identity.Extensions;
using RecordedCourseSellingApp.Services;
using RecordedCourseSellingApp.Web.Entities;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("ApplicationDb");

builder.Services
    .AddDataAccessLayer(connectionString)
    .AddServiceLayer();

builder.Logging.ClearProviders();
builder.Logging.AddLog4Net();

// Add services to the container.
builder.Services.AddControllersWithViews();

var log = LogManager.GetLogger(typeof(Program));

try
{
    var app = builder.Build();
    log.Info("Application Starting up..");

    // Configure the HTTP request pipeline.
    if (!app.Environment.IsDevelopment())
    {
        app.UseExceptionHandler("/Home/Error");
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
    }

    app.UseHttpsRedirection();
    app.UseStaticFiles();

    app.UseRouting();

    app.UseAuthentication();
    app.UseAuthorization();

    app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");

    app.Run();
}
catch (Exception ex)
{
    log.Fatal($"Exception Message: {ex.Message}\nException: {ex}");
}
