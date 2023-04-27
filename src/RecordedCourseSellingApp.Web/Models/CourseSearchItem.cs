using Microsoft.AspNetCore.Mvc.Rendering;
using RecordedCourseSellingApp.Shared.Enums;
using System.ComponentModel.DataAnnotations;

namespace RecordedCourseSellingApp.Web.Models;

public class CourseSearchItem
{
    [Display(Name = "Category")]
    public Guid? CategoryId { get; set; }

    [Display(Name = "Difficulty Level")]
    public DifficultyLevel DifficultyLevel { get; set; }

    [Display(Name = "Search Keyword")]
    public string? SearchText { get; set; }

    public IEnumerable<SelectListItem>? CategoryDdl { get; set; }

    public IEnumerable<SelectListItem>? DificultyLevelDdl { get; set; }

}
