using Domain.Core;
using Domain.Repositories;

namespace Domain.Students;

public class UnregisterCommand : ICommand
{
    public long Id { get; set; }
    public UnregisterCommand(long id) => Id = id;

    public sealed class UnregisterCommandHanlder : ICommandHandler<UnregisterCommand>
    {
        private readonly IStudentRepository studentRepository;

        public UnregisterCommandHanlder(IStudentRepository studentRepository) => this.studentRepository = studentRepository;

        public async Task<ResponseResult> HandleAsync(UnregisterCommand command)
        {
            var studentInDb = await studentRepository.GetByIdAsync(command.Id);
            if (studentInDb is not null)
            {
                studentRepository.Delete(studentInDb);
                _ = await studentRepository.CommitAsync();
            }

            return new ResponseResult { Type = ResponseType.Success };
        }
    }
}
