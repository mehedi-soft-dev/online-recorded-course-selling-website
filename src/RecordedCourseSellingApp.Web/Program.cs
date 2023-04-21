using log4net;
using RecordedCourseSellingApp.DataAccess;
using RecordedCourseSellingApp.Services;
using RecordedCourseSellingApp.Web;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("ApplicationDb");

builder.Services
    .AddNHibernate(connectionString)
    .AddServices()
    .AddDataAccess();

builder.Logging.ClearProviders();
builder.Logging.AddLog4Net();

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

ILog log = LogManager.GetLogger(typeof(Program));
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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();