using Domain.Core;
using Domain.Courses;
using Domain.Repositories;

namespace Domain.Students;

public sealed class EnrollCommand : ICommand
{
    public long Id { get;  }
    public string? Course { get; }
    public string? Grade { get; }

    public EnrollCommand(long id, string? course, string? grade)
    {
        Id = id;
        Course = course;
        Grade = grade;
    }

    public sealed class EnrollCommandHandler : ICommandHandler<EnrollCommand>
    {
        private readonly IStudentRepository studentRepository;
        private readonly ICourseRepository courseRepository;

        public EnrollCommandHandler(IStudentRepository studentRepository, ICourseRepository courseRepository)
        {
            this.studentRepository = studentRepository;
            this.courseRepository = courseRepository;
        }
        public async Task<ResponseResult> HandleAsync(EnrollCommand command)
        {
            var student = await studentRepository.GetByIdAsync(command.Id);
            if (student is null)
            {
                return new ResponseResult
                {
                    Type = ResponseType.Failure,
                    ErrorMessage = $"No student found for Id {command.Id}"
                };
            }

            Course? course = await courseRepository.GetByNameAsync(command.Course);
            if (course == null)
            {
                return new ResponseResult
                {
                    Type = ResponseType.Failure,
                    ErrorMessage = $"Course is incorrect: {command.Course}"
                };
            }

            bool success = Enum.TryParse(command.Grade, out Grade grade);
            if (!success)
            {
                return new ResponseResult
                {
                    Type = ResponseType.Failure,
                    ErrorMessage = $"Grade is incorrect: {command.Grade}"
                };
            }

            student.Enroll(course, grade);
            await studentRepository.CommitAsync();
            return new ResponseResult { Type = ResponseType.Success };
        }
    }
}
