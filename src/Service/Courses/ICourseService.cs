namespace Service.Courses;

using Domain.Courses;
using Domain.Dtos;

public interface ICourseService
{
    Task CreateAsync(CourseDto course);
    Task<Course?> GetByNameAsync(string? courseName);
}
