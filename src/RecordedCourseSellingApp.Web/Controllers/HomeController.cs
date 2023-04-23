using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using RecordedCourseSellingApp.Services.DTOs;
using RecordedCourseSellingApp.Services.Services;
using RecordedCourseSellingApp.Web.Models;

namespace RecordedCourseSellingApp.Web.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ICustomerService _customerService;

    public HomeController(ILogger<HomeController> logger, ICustomerService customerService)
    {
        _logger = logger;
        _customerService = customerService;
    }

    public IActionResult Index()
    {
        _logger.LogInformation("In index page");
        
        //_customerService.Insert(new CustomerDto()
        //{
        //    Email = "cse.mdmehedi@gmail.com",
        //    Name = "Mehedi Hasan",
        //});
        
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}