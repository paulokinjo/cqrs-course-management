using Domain.Core;
using Domain.Courses;
using Domain.Dtos;
using Domain.Repositories;

namespace Domain.Students;

public class RegisterCommand : ICommand
{
    public string? Name { get; }
    public string? Email { get; }
    public string? Course1 { get; }
    public string? Course1Grade { get; }
    public string? Course2 { get; }
    public string? Course2Grade { get; }

    public RegisterCommand(string? name, string? email, string? course1, string? course1Grade, string? course2, string? course2Grade)
    {
        Name = name;
        Email = email;
        Course1 = course1;
        Course1Grade = course1Grade;
        Course2 = course2;
        Course2Grade = course2Grade;
    }

    public sealed class RegisterCommandHanlder : ICommandHandler<RegisterCommand>
    {
        private readonly IStudentRepository studentRepository;
        private readonly ICourseRepository courseRepository;

        public RegisterCommandHanlder(IStudentRepository studentRepository, ICourseRepository courseRepository)
        {
            this.studentRepository = studentRepository;
            this.courseRepository = courseRepository;
        }

        public async Task<ResponseResult> HandleAsync(RegisterCommand command)
        {
            var student = new Student(command.Name, command.Email);
            if (command.Course1 != null && command.Course1Grade != null)
            {
                Course? course = await courseRepository.GetByNameAsync(command.Course1);
                if (course is not null)
                {
                    student.Enroll(course, Enum.Parse<Grade>(command.Course1Grade));
                }
            }

            if (command.Course2 != null && command.Course2Grade != null)
            {
                Course? course = await courseRepository.GetByNameAsync(command.Course2);
                if (course is not null)
                {
                    student.Enroll(course, Enum.Parse<Grade>(command.Course2Grade));
                }
            }

            await studentRepository.CreateAsync(student);
            _ = await studentRepository.CommitAsync();

            return new ResponseResult { Type = ResponseType.Success };
        }
    }
}
