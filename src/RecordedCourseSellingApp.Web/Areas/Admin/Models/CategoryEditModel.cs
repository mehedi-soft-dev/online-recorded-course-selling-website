using Autofac;
using Mapster;
using RecordedCourseSellingApp.Services.BusinessObjects;
using RecordedCourseSellingApp.Services.Services;
using RecordedCourseSellingApp.Web.Models;
using System.ComponentModel.DataAnnotations;

namespace RecordedCourseSellingApp.Web.Areas.Admin.Models;

public class CategoryEditModel : BaseModel
{
    public Guid Id { get; set; }

    [Required(ErrorMessage = "Name must be provided"),
        StringLength(200, ErrorMessage = "Name should be less than 200 characters")]
    public string Name { get; set; } = string.Empty;

    public string? Description { get; set; }

    [Display(Name = "Is Active")]
    public bool IsActive { get; set; }

    private ICategoryService _categoryService;

    public CategoryEditModel()
    {
        
    }

    public CategoryEditModel(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    public override void ResolveDependency(ILifetimeScope scope)
    {
        base.ResolveDependency(scope);
        _categoryService = _scope.Resolve<ICategoryService>();
    }

    internal async Task LoadDataAsync(Guid id)
    {
        var category = await _categoryService.GetCategoryByIdAsync(id);
        
        if (category is null)
            throw new Exception("Category not found");
     
        category.Adapt(this);
    }

    internal async Task EditCategoryAsync()
    {
        var category = this.Adapt<Category>();
        await _categoryService.EditCategoryAsync(category);
    }
}
