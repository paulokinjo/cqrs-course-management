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
    private Envelop(string result, string? errorMessage) : base(result, errorMessage) { }

    public static Envelop<T> Ok<T>(T result) => new Envelop<T>(result, null);

    public static Envelop Ok(string result) => new Envelop(result, null);

    public static Envelop Error(string result, string? errorMessage) => new Envelop(result, errorMessage);
}
