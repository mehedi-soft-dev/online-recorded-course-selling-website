using Microsoft.AspNetCore.Mvc.Rendering;
using RecordedCourseSellingApp.Services.BusinessObjects;
using System.Linq.Expressions;
using CategoryEO = RecordedCourseSellingApp.DataAccess.Entities.Category;

namespace RecordedCourseSellingApp.Services.Services;

public interface ICategoryService
{
    Task CreateCategoryAsync(Category category);

    Task<(int total, int totalDisplay, IList<Category> records)> GetCategoriesByPagingAsync(int pageIndex, 
        int pageSize, string searchText, string orderby);

    Task DeleteCategoryAsync(Guid id);

    Task<int> GetCategoryCountAsync(Expression<Func<CategoryEO, bool>>? predicate = null!);

    Task<Category?> GetCategoryByIdAsync(Guid id);

    Task EditCategoryAsync(Category category);

    Task<IList<Category>> GetAllCategoriesAsync();

    Task<IEnumerable<SelectListItem>> GetCategoriesAsDdlAsync();
}
