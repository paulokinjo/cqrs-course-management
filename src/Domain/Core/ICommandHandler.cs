namespace Domain.Core;

public interface ICommandHandler<TCommand> where TCommand : ICommand
{
    Task<ResponseResult> HandleAsync(TCommand command);
}