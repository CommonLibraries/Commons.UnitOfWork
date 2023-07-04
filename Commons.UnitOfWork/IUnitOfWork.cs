using System.Data;

namespace Commons.UnitOfWork
{
    public interface IUnitOfWork : IDisposable, IAsyncDisposable
    {
        bool IsDisposed { get; }

        IDbCommand CreateCommand(string commandText = "");

        void Begin(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted);
        Task BeginAsync(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted, CancellationToken cancellationToken = default);

        void Commit();
        Task CommitAsync(CancellationToken cancellationToken = default);

        void Rollback();
        Task RollbackAsync(CancellationToken cancellationToken = default);
    }

    public abstract class Repository<TEntity> where TEntity : class
    {

    }
}
