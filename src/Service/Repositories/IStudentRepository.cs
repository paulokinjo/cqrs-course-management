namespace Service.Repositories;
using Domain.Students;

public interface IStudentRepository : IUnityOfWork
{
    Task CreateAsync(Student student);
    void Delete(Student student);
    Task<Student?> GetByIdAsync(long id);
    Task<IReadOnlyList<Student>> GetListAsync(string? enrolledIn, int? numberOfCourses);
}
