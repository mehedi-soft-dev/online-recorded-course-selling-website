using Autofac;
using Mapster;
using RecordedCourseSellingApp.Services.BusinessObjects;
using RecordedCourseSellingApp.Services.Services;
using RecordedCourseSellingApp.Web.Models;
using System.ComponentModel.DataAnnotations;

namespace RecordedCourseSellingApp.Web.Areas.Admin.Models;

public class CategoryCreateModel : BaseModel
{
    [Required(ErrorMessage = "Name must be provided"),
            StringLength(200, ErrorMessage = "Name should be less than 200 characters")]
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }

    [Display(Name ="Is Active")]
    public bool IsActive { get; set; }
    
    private ICategoryService? _categoryService;

    public CategoryCreateModel() : base()
    {

    }

    public CategoryCreateModel(ICategoryService coursService)
    {
        _categoryService = coursService;
    }

    public override void ResolveDependency(ILifetimeScope scope)
    {
        base.ResolveDependency(scope);
        _categoryService = _scope.Resolve<ICategoryService>();
    }

    internal async Task CreateCategoryAsync()
    {
        Category course = this.Adapt<Category>();
        await _categoryService!.CreateCategoryAsync(course);
    }
}
