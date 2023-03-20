namespace Api.Utils;

public class Envelop<T>
{
    public T? Result { get; }
    public string? ErrorMessage { get; }
    public DateTimeOffset? TimeGenerated { get; }

    protected internal Envelop(T? result, string? errorMessage)
    {
        Result = result;
        ErrorMessage = errorMessage;
        TimeGenerated = DateTimeOffset.UtcNow;
    }
}

public sealed class Envelop : Envelop<string> 
{
    private Envelop(string? errorMessage) : base(null, errorMessage) { }

    public static Envelop<T> Ok<T>(T result) => new Envelop<T>(result, null);

    public static Envelop Ok() => new Envelop(null);

    public static Envelop Error(string? errorMessage) => new Envelop(errorMessage);
}
