using Domain.Courses;
using Microsoft.EntityFrameworkCore;
using Domain.Repositories;

namespace Data.Courses;

internal class CourseRepository : BaseRepository, ICourseRepository
{
    public CourseRepository(CourseManagementDbContext context) : base(context) { }

    public async Task CreateAsync(Course course) => await Context.Courses.AddAsync(course);

    public async Task<Course?> GetByNameAsync(string? courseName)
    {
        var courses = Context.Courses.ToList();
        return await Context.Courses.FirstOrDefaultAsync(x => x.Name == courseName);
    }
}
