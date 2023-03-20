namespace Service.Repositories;

public interface IUnityOfWork
{
    Task<bool> CommitAsync();
}
