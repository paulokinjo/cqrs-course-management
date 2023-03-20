namespace Data;

using Microsoft.EntityFrameworkCore;
using Domain.Students;
using Domain.Courses;

internal class CourseManagementDbContext : DbContext
{
    public DbSet<Student> Students => Set<Student>();
    public DbSet<Course> Courses => Set<Course>();

    public CourseManagementDbContext(DbContextOptions<CourseManagementDbContext> options)
      : base(options)
    {

    }
}
