using Autofac;
using Mapster;
using Microsoft.AspNetCore.Mvc.Rendering;
using RecordedCourseSellingApp.Services.BusinessObjects;
using RecordedCourseSellingApp.Services.Common;
using RecordedCourseSellingApp.Services.Services;
using RecordedCourseSellingApp.Shared.Enums;
using RecordedCourseSellingApp.Web.Models;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace RecordedCourseSellingApp.Web.Areas.Admin.Models;

public class CourseEditModel : BaseModel
{
    public Guid Id { get; set; }

    [Required(ErrorMessage = "Name must be provided"),
            StringLength(200, ErrorMessage = "Name should be less than 200 characters")]
    public string Title { get; set; } = string.Empty;

    [Display(Name = "Difficulty Level")]
    [Required(ErrorMessage = "Difficulty Level field required.")]
    public DifficultyLevel DifficultyLevel { get; set; }

    public string? Description { get; set; }

    [Display(Name = "Course Thumbnail Image")]
    public string? ThumbnailImage { get; set; }

    [Display(Name = "Course Video Url")]
    [Required(ErrorMessage = "Course video URL required.")]
    public string VideoUrl { get; set; }

    [Display(Name = "Course Price")]
    [Range(5000, 100000, ErrorMessage = "Course Price must be between 5000 and 100000.")]
    public int Price { get; set; }

    [Display(Name = "Category")]
    [Required(ErrorMessage = "Category field required.")]
    public Guid CategoryId { get; set; }

    public IEnumerable<SelectListItem>? CategoryDdl { get; private set; }

    public IEnumerable<SelectListItem>? DificultyLevelDdl { get; private set; }

    private ICourseService? _courseService;

    private ICategoryService? _categoryService;

    private IBufferedFileUploadService _bufferedFileUploadService;

    public CourseEditModel() : base()
    {

    }

    public CourseEditModel(ICourseService courseService,
        ICategoryService categoryService,
        IBufferedFileUploadService bufferedFileUploadService)
    {
        _courseService = courseService;
        _categoryService = categoryService;
        _bufferedFileUploadService = bufferedFileUploadService;
    }

    public override void ResolveDependency(ILifetimeScope scope)
    {
        base.ResolveDependency(scope);
        _courseService = _scope.Resolve<ICourseService>();
        _bufferedFileUploadService = _scope.Resolve<IBufferedFileUploadService>();
    }

    internal async Task EditCourseAsync()
    {
        Course course = this.Adapt<Course>();

        await _courseService!.EditCourseAsync(course);
    }

    internal async Task LoadDdlAsync()
    {
        CategoryDdl = await _categoryService!.GetCategoriesAsDdlAsync();
        DificultyLevelDdl = DdlService.DifficultyLevelDdl;
    }

    internal async Task<string?> UploadFileAsync(IFormFile file)
    {
        return await _bufferedFileUploadService.UploadFileAsync(file);
    }

    internal async Task LoadDataAsync(Guid id)
    {
        var course = await _courseService!.GetCourseByIdAsync(id);

        if (course is null)
            throw new Exception("Course not found");

        course.Adapt(this);
    }
}
