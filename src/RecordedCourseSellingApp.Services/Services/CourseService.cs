﻿using Mapster;
using RecordedCourseSellingApp.DataAccess.UnitOfWorks;
using RecordedCourseSellingApp.Services.BusinessObjects;
using RecordedCourseSellingApp.Shared.Exceptions;
using CourseEO = RecordedCourseSellingApp.DataAccess.Entities.Course;

namespace RecordedCourseSellingApp.Services.Services;

internal class CourseService : ICourseService
{
    private readonly IUnitOfWork _unitOfWork;

    public CourseService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task CreateCourseAsync(Course course)
    {
        var searchedObject = await _unitOfWork.Courses.GetSingleAsync(x => x.Title == course.Title);

        if (searchedObject is not null)
            throw new DuplicationExeption("Course name already exits");

        var entity = course.Adapt<CourseEO>();

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

        if (entity is null)
            throw new Exception("Course Not Found");

        course.Adapt(entity);

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
            .GetByPagingAsync(x => x.Title.Contains(searchText), orderby, pageIndex, pageSize);

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
}