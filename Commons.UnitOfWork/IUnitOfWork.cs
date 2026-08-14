using Commons.UnitOfWork.TransactionInterceptors;
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

public abstract class TransactionInterceptor : ITransactionInterceptor
{
    public virtual Task TransactionStartingAsync(IUnitOfWork unitOfWork, CancellationToken cancellationToken = default)
        => Task.CompletedTask;
    public virtual Task TransactionStartedAsync(IUnitOfWork unitOfWork, CancellationToken cancellationToken = default)
        => Task.CompletedTask;
    public virtual Task TransactionCommitingAsync(IUnitOfWork unitOfWork, CancellationToken cancellationToken = default)
        => Task.CompletedTask;
    public virtual Task TransactionCommitedAsync(IUnitOfWork unitOfWork, CancellationToken cancellationToken = default)
        => Task.CompletedTask;
    public virtual Task TransactionRollingBackAsync(IUnitOfWork unitOfWork, CancellationToken cancellationToken = default)
        => Task.CompletedTask;
    public virtual Task TransactionRolledBackAsync(IUnitOfWork unitOfWork, CancellationToken cancellationToken = default)
        => Task.CompletedTask;

    public virtual void TransactionStarting(IUnitOfWork unitOfWork)
    {
        return;
    }

    public virtual void TransactionStarted(IUnitOfWork unitOfWork)
    {
        return;
    }

    public virtual void TrnasactionCommitting(IUnitOfWork unitOfWork)
    {
        return;
    }

    public virtual void TransactionCommitted(IUnitOfWork unitOfWork)
    {
        return;
    }

    public virtual void TransactionRollingBack(IUnitOfWork unitOfWork)
    {
        return;
    }

    public virtual void TransactionRolledBack(IUnitOfWork unitOfWork)
    {
        return;
    }
}
