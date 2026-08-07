using System.Data;

namespace Commons.UnitOfWork;

public interface IUnitOfWork : IDisposable, IAsyncDisposable
{
    string? ContextKey { get; }
    IDbConnection Connection { get; }
    IDbTransaction? Transaction { get; }
    UnitOfWorkStatus Status { get; }

    void Begin();
    Task BeginAsync(CancellationToken cancellationToken = default);

    void Commit();
    Task CommitAsync(CancellationToken cancellationToken = default);

    void Rollback();
    Task RollbackAsync(CancellationToken cancellationToken = default);
}
