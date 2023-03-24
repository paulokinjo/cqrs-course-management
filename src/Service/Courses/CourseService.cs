namespace Service.Courses;

using Domain.Courses;
using Domain.Repositories;

internal class CourseService : ICourseService
{
    private readonly ICourseRepository repository;

    public CourseService(ICourseRepository repository) => this.repository = repository;

    public async Task CreateAsync(CourseDto course)
    {
        await repository.CreateAsync(new Course(course.Name, course.Credits));
        _ = await repository.CommitAsync();
    }

    public async Task<Course?> GetByNameAsync(string? courseName) => 
        await repository.GetByNameAsync(courseName);
}