using Domain.Courses;

namespace Service.Repositories;

public interface ICourseRepository : IUnityOfWork
{ 
    Task CreateAsync(Course course);
    Task<Course?> GetByNameAsync(string? courseName);
}