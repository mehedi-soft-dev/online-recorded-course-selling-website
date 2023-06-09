﻿namespace RecordedCourseSellingApp.DataAccess.Entities;

public class Category : IEntity<Guid>
{
    public virtual Guid Id { get; set; }

    public virtual string Name { get; set; } = string.Empty;

    public virtual string? Description { get; set; }

    public virtual bool IsActive { get; set; }

    public virtual IList<Course> Courses { get; set;}
}
