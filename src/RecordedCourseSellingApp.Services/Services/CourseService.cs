﻿using Mapster;
using RecordedCourseSellingApp.DataAccess.UnitOfWorks;
using RecordedCourseSellingApp.Services.BusinessObjects;
using RecordedCourseSellingApp.Services.DTOs;
using RecordedCourseSellingApp.Shared.Enums;
using RecordedCourseSellingApp.Shared.Exceptions;
using System.Linq.Expressions;
using CourseEO = RecordedCourseSellingApp.DataAccess.Entities.Course;

namespace RecordedCourseSellingApp.Services.Services;

internal class CourseService : ICourseService
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly ICategoryService _categoryService;

	public CourseService(IUnitOfWork unitOfWork, ICategoryService categoryService)
	{
		_unitOfWork = unitOfWork;
		_categoryService = categoryService;
	}

	public async Task CreateCourseAsync(Course course)
	{
		var courseEntity = await _unitOfWork.Courses.GetSingleAsync(x => x.Title == course.Title);
		var category = await _unitOfWork.Categories.GetSingleAsync(course.CategoryId);

		if (courseEntity is not null)
			throw new DuplicationExeption("Course name already exits");

		if (category is null)
			throw new Exception("Category not found");

		var entity = course.Adapt<CourseEO>();
		entity.Category = category;

		await _unitOfWork.BeginTransaction();
		await _unitOfWork.Courses.AddAsync(entity);
		await _unitOfWork.Commit();
	}

	public async Task DeleteCourseAsync(Guid id)
	{
		var entity = await _unitOfWork.Courses.GetSingleAsync(id);

		if (entity is null)
			throw new Exception("Course not found");

		await _unitOfWork.BeginTransaction();
		await _unitOfWork.Courses.DeleteAsync(entity);
		await _unitOfWork.Commit();
	}

	public async Task EditCourseAsync(Course course)
	{
		var entity = await _unitOfWork.Courses.GetSingleAsync(course.Id);
		var category = await _unitOfWork.Categories.GetSingleAsync(course.CategoryId);

		if (entity is null)
			throw new Exception("Course Not Found");

		if (category is null)
			throw new Exception("Category Not found");

		course.Adapt(entity);
		entity.Category = category;

		await _unitOfWork.BeginTransaction();
		await _unitOfWork.Courses.AddOrUpdateAsync(entity);
		await _unitOfWork.Commit();
	}

	public async Task<IList<Course>> GetAllCoursesAsync()
	{
		throw new NotImplementedException();
	}

	public async Task<(int total, int totalDisplay, IList<Course> records)>
		GetCoursesByPagingAsync(int pageIndex, int pageSize, string searchText, string orderby)
	{
		var results = await _unitOfWork
			.Courses
			.GetByPagingAsync(x => x.Title.Contains(searchText), orderby, pageIndex, pageSize, x => x.Category);

		var courses = new List<Course>();

		foreach (var course in results.data)
		{
			courses.Add(course.Adapt<Course>());
		}

		return (results.total, results.totalDisplay, courses);
	}

	public async Task<Course?> GetCourseByIdAsync(Guid id)
	{
		var course = await _unitOfWork.Courses.GetSingleAsync(id);

		return course!.Adapt<Course>();
	}

	public async Task<IList<CourseDto>> GetCoursesBySearchAsync(Guid? categoryId = null,
		DifficultyLevel? difficultyLevel = null!, string? searchText = null)
	{
		var courses = await _unitOfWork.Courses.FindAsync(
			x => (categoryId == null ? true : x.Category.Id == categoryId) &&
				 (difficultyLevel > 0 ? x.DifficultyLevel == difficultyLevel : true) &&
				 (string.IsNullOrEmpty(searchText) ? true : x.Title.Contains(searchText!)));

		IList<CourseDto> result = new List<CourseDto>();

		foreach(var course in courses)
		{
			result.Add(course.Adapt<CourseDto>());
		}

		return result;
	}

    public async Task<int> GetCoursesCountAsync(
		Expression<Func<CourseEO, bool>>? predicate = null)
    {
		return await _unitOfWork.Courses.GetCountAsync(predicate!);
    }
}
