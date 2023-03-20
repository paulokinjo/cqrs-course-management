namespace Data.Students;

using Service.Repositories;
using Domain.Students;
using Microsoft.EntityFrameworkCore;

internal class StudentRepository : BaseRepository, IStudentRepository
{
    public StudentRepository(CourseManagementDbContext context) : base(context) {}

    public async Task CreateAsync(Student student) => await Context.Students.AddAsync(student);

    public void Delete(Student student) => Context.Students.Remove(student);

    public async Task<Student?> GetByIdAsync(long id) => await Context.Students.Include(x => x.Enrollments).ThenInclude(x => x.Course).FirstOrDefaultAsync(x => x.Id == id);

    public Task<IReadOnlyList<Student>> GetListAsync(string? enrolledIn, int? numberOfCourses)
    {
        IQueryable<Student> query = Context.Students.AsQueryable();

        if (!string.IsNullOrWhiteSpace(enrolledIn))
        {
            query = query.Where(x => x.Enrollments.Any(e => (e != null!) && (e.Course != null!) && e.Course.Name == enrolledIn));
        }

        IReadOnlyList<Student> result = query.Include(x => x.Enrollments).ThenInclude(x => x.Course).ToList();
        if (numberOfCourses != null)
        {
            result = result.Where(x => x.Enrollments.Count == numberOfCourses).ToList();
        }

        return Task.FromResult(result);
    }    
}
