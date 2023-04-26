using Mapster;
using Microsoft.AspNetCore.Mvc.Rendering;
using RecordedCourseSellingApp.DataAccess.UnitOfWorks;
using RecordedCourseSellingApp.Services.BusinessObjects;
using RecordedCourseSellingApp.Shared.Exceptions;
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
        var searchedObject = await _unitOfWork.Categories.GetSingleAsync(x => x.Name == category.Name);

        if (searchedObject is not null)
            throw new DuplicationExeption("Category name already exits");

        var entity = category.Adapt<CategoryEO>();

        await _unitOfWork.BeginTransaction();
        await _unitOfWork.Categories.AddAsync(entity);
        await _unitOfWork.Commit();
    }

    public async Task DeleteCategoryAsync(Guid id)
    {
        var entity = await _unitOfWork.Categories.GetSingleAsync(id);

        if (entity is null)
            throw new Exception("Category not found");

        await _unitOfWork.BeginTransaction();
        await _unitOfWork.Categories.DeleteAsync(entity);
        await _unitOfWork.Commit();
    }

    public async Task EditCategoryAsync(Category category)
    {
        var entity = await _unitOfWork.Categories.GetSingleAsync(category.Id);

        if (entity is null)
            throw new Exception("Category Not Found");

        category.Adapt(entity);

        await _unitOfWork.BeginTransaction();
        await _unitOfWork.Categories.AddOrUpdateAsync(entity);
        await _unitOfWork.Commit();
    }

    public async Task<IList<Category>> GetAllCategoriesAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<(int total, int totalDisplay, IList<Category> records)> 
        GetCategoriesByPagingAsync(int pageIndex, int pageSize, string searchText, string orderby)
    {
        var results = await _unitOfWork
            .Categories
            .GetByPagingAsync(x => x.Name.Contains(searchText), orderby, pageIndex, pageSize);

        var categories = new List<Category>();

        foreach (var category in results.data)
        {
            categories.Add(category.Adapt<Category>());
        }

        return (results.total, results.totalDisplay, categories);
    }

    public async Task<Category?> GetCategoryByIdAsync(Guid id)
    {
        var category = await _unitOfWork.Categories.GetSingleAsync(id);

        return category!.Adapt<Category>();
    }

    public async Task<IEnumerable<SelectListItem>> GetCategoriesAsDdlAsync()
    {
        var categories = await _unitOfWork.Categories.GetAllAsync();

        IEnumerable<SelectListItem> datas = categories.Select(n => new SelectListItem
        {
            Value = n.Id.ToString(),
            Text = n.Name,
        }).Distinct().ToList();

        return new SelectList(datas, "Value", "Text");
    }
}
