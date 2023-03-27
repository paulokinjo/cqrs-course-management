namespace Domain.Core;

public sealed class Messages
{
    private readonly IServiceProvider provider;

    public Messages(IServiceProvider provider) => this.provider = provider;

    public async Task<ResponseResult> DispatchAsync(ICommand command)
    {
        Type type = typeof(ICommandHandler<>);
        Type[] typeArgs = { command.GetType() };
        Type handlerType = type.MakeGenericType(typeArgs);

        dynamic? handler = provider.GetService(handlerType);
        if (handler == null)
        {
            return new ResponseResult 
            {
                Type = ResponseType.Failure,
                ErrorMessage = "Fail to get service handler for message dispatching"
            };
        }

        ResponseResult result = await handler.HandleAsync((dynamic)command);
        return result;
    }

    public async Task<T?> DispatchAsync<T>(IQuery<T> query)
    {
        Type type = typeof(IQueryHandler<,>);
        Type[] typeArgs = { query.GetType(), typeof(T) };
        Type handlerType = type.MakeGenericType(typeArgs);

        dynamic? handler = provider.GetService(handlerType);

        if (handler == null)
        {
            return default;
        }

        T result = await handler.HandleAsync((dynamic)query);
        return result;
    }
}
