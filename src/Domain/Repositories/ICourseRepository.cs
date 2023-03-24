using Domain.Courses;

namespace Domain.Repositories;

public interface ICourseRepository : IUnityOfWork
{ 
    Task CreateAsync(Course course);
    Task<Course?> GetByNameAsync(string? courseName);
}