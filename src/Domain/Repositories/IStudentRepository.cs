using Domain.Students;

namespace Domain.Repositories;

public interface IStudentRepository : IUnityOfWork
{
    Task CreateAsync(Student student);
    void Delete(Student student);
    Task<Student?> GetByIdAsync(long id);
    Task<IReadOnlyList<Student>> GetListAsync(string? enrolledIn, int? numberOfCourses);
}
