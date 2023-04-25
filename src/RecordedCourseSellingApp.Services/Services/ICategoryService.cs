using RecordedCourseSellingApp.Services.BusinessObjects;

namespace RecordedCourseSellingApp.Services.Services;

public interface ICategoryService
{
    Task CreateCategoryAsync(Category category);

    Task<(int total, int totalDisplay, IList<Category> records)> GetCategoriesByPagingAsync(int pageIndex, 
        int pageSize, string searchText, string orderby);

    Task<(int total, int totalDisplay, IList<Category> records)> GetCategoriesByPagingAdvancedAsync(
        int pageIndex, int pageSize, string name, string orderby);

    Task DeleteCategoryAsync(Guid id);

    Task<Category?> GetCategoryByIdAsync(Guid id);

    Task EditCategoryAsync(Category category);

    Task<IList<Category>> GetAllCategoriesAsync();

    Task<Category> GetCategoryByNameAsync(string name);
}
