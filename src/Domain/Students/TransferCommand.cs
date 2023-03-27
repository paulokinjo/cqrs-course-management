using Domain.Core;
using Domain.Courses;
using Domain.Repositories;

namespace Domain.Students;

public class TransferCommand : ICommand
{
    public long Id { get; }
    public int EnrollmentNumber { get; }
    public string? Course { get; }
    public string? Grade { get; }

    public TransferCommand(long id, int enrollmentNumber, string? course, string? grade)
    {
        Id = id;
        EnrollmentNumber = enrollmentNumber;
        Course = course;
        Grade = grade;
    }

    public sealed class TransferCommandHandler : ICommandHandler<TransferCommand>
    {
        private readonly IStudentRepository studentRepository;
        private readonly ICourseRepository courseRepository;

        public TransferCommandHandler(IStudentRepository studentRepository, ICourseRepository courseRepository)
        {
            this.studentRepository = studentRepository;
            this.courseRepository = courseRepository;
        }
        public async Task<ResponseResult> HandleAsync(TransferCommand command)
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

            Enrollment enrollment = student.GetEnrollment(command.EnrollmentNumber);
            if (enrollment == null)
            {
                return new ResponseResult
                {
                    Type = ResponseType.Failure,
                    ErrorMessage = $"No enrollment found with number: {command.EnrollmentNumber}"
                };
            }

            enrollment.Update(course, grade);
            await studentRepository.CommitAsync();

            return new ResponseResult { Type = ResponseType.Success };
        }
    }
}
