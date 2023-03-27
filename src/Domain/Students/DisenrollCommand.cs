using Domain.Core;
using Domain.Courses;
using Domain.Dtos;
using Domain.Repositories;
using System.ComponentModel.Design;

namespace Domain.Students;

public sealed partial class DisenrollCommand : ICommand
{
    public long Id { get; }
    public int EnrollmentNumber { get; }
    public string? Comment { get; }

    public DisenrollCommand(long id, int enrollmentNumber, string? comment)
    {
        Id = id;
        EnrollmentNumber = enrollmentNumber;
        Comment = comment;
    }

    public sealed class DisenrollCommandHandler : ICommandHandler<DisenrollCommand>
    {
        private readonly IStudentRepository studentRepository;
        public DisenrollCommandHandler(IStudentRepository studentRepository) => this.studentRepository = studentRepository;
        public async Task<ResponseResult> HandleAsync(DisenrollCommand command)
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

            if (string.IsNullOrWhiteSpace(command.Comment))
            {
                return new ResponseResult
                {
                    Type = ResponseType.Failure,
                    ErrorMessage = "Disenrollment comment is required"
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

            student.RemoveEnrollment(enrollment, command.Comment);
            await studentRepository.CommitAsync();

            return new ResponseResult { Type = ResponseType.Success };
        }
    }
}
