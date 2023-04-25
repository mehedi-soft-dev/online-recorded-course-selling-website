using Mapster;
using RecordedCourseSellingApp.DataAccess.UnitOfWorks;
using RecordedCourseSellingApp.Services.BusinessObjects;
using CategoryEO = RecordedCourseSellingApp.DataAccess.Entities.Category;

namespace RecordedCourseSellingApp.Services.Services;

public class CategoryService : ICategoryService
{
    private readonly IUnitOfWork _unitOfWork;

    public CategoryService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task CreateCategoryAsync(Category category)
    {
        var entity = category.Adapt<CategoryEO>();

        await _unitOfWork.BeginTransaction();
        await _unitOfWork.Categories.AddAsync(entity);
        await _unitOfWork.Commit();
    }

    public async Task DeleteCategoryAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task EditCategoryAsync(Category category)
    {
        throw new NotImplementedException();
    }

    public async Task<IList<Category>> GetAllCategoriesAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<(int total, int totalDisplay, IList<Category> records)> GetCategoriesByPagingAdvancedAsync(int pageIndex, int pageSize, string name, string orderby)
    {
        throw new NotImplementedException();
    }

    public async Task<(int total, int totalDisplay, IList<Category> records)> GetCategoriesByPagingAsync(int pageIndex, int pageSize, string searchText, string orderby)
    {
        throw new NotImplementedException();
    }

    public async Task<Category?> GetCategoryByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task<Category> GetCategoryByNameAsync(string name)
    {
        throw new NotImplementedException();
    }
}
