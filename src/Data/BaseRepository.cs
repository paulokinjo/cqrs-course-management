using Service.Repositories;

namespace Data;

internal abstract class BaseRepository : IUnityOfWork, IDisposable
{
    public CourseManagementDbContext Context { get; }
    private bool disposed = false;

    public BaseRepository(CourseManagementDbContext context) => Context = context;

    public async Task<bool> CommitAsync() => await Context.SaveChangesAsync() > 0;

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!disposed)
        {
            if (disposing)
            {
                Context.Dispose();
            }
        }

        disposed = true;
    }

}
