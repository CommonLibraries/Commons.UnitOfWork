using System.Data;

namespace Commons.UnitOfWork
{
    public interface IUnitOfWork : IDisposable, IAsyncDisposable
    {
        bool IsDisposed { get; }

        void Commit();
        Task CommitAsync(CancellationToken cancellationToken = default);

        void Rollback();
        Task RollbackAsync(CancellationToken cancellationToken = default);
    }
}
