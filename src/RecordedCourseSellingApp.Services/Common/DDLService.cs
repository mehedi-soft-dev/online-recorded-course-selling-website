using Microsoft.AspNetCore.Mvc.Rendering;
using RecordedCourseSellingApp.Shared.Enums;

namespace RecordedCourseSellingApp.Services.Common;

public class DdlService
{
    public static IEnumerable<SelectListItem> DifficultyLevelDdl
    {
        get
        {
            return Enum.GetValues(typeof(DifficultyLevel)).Cast<DifficultyLevel>()
                .Select(v => new SelectListItem
            {
                Text = v.ToString(),
                Value = ((int)v).ToString()
            }).ToList();
        }
    }
}
