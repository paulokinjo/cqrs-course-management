namespace Service.Courses;

using Domain.Courses;

public interface ICourseService
{
    Task CreateAsync(CourseDto course);
    Task<Course?> GetByNameAsync(string? courseName);
}
