using Domain.Core;
using Domain.Repositories;
using System.ComponentModel.Design;

namespace Domain.Students;

public sealed class EditPersonalInfoCommand : ICommand
{
    public long Id { get; set; }
    public string? Name { get; set; }
    public string? Email { get; set; }

    public sealed class EditPersonalInfoCommandHandler : ICommandHandler<EditPersonalInfoCommand>
    {
        private readonly IStudentRepository studentRepository;

        public EditPersonalInfoCommandHandler(IStudentRepository studentRepository)
        {
            if (studentRepository is null)
            {
                throw new ArgumentNullException(nameof(studentRepository));
            }

            this.studentRepository = studentRepository;
        }


        public async Task<ResponseResult> HandleAsync(EditPersonalInfoCommand command)
        {
            Student? student = await studentRepository.GetByIdAsync(command.Id);

            if (student is null)
            {
                return new ResponseResult
                {
                    Type = ResponseType.Failure,
                    ErrorMessage = $"No student found for Id {command.Id}"
                };
            }

            student.Name = command.Name;
            student.Email = command.Email;

            await studentRepository.CommitAsync();

            return new ResponseResult { Type = ResponseType.Success };
        }
    }
}
