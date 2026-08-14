using Commons.UnitOfWork.TransactionInterceptors;
using System.Data;

namespace Commons.UnitOfWork.Extensions.TransactionInterceptors;

internal class DefaultUnitOfWorkWithTransactionInterceptors : IUnitOfWork
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IEnumerable<ITransactionInterceptor> interceptors;
    public DefaultUnitOfWorkWithTransactionInterceptors(
        IUnitOfWork unitOfWork,
        IEnumerable<ITransactionInterceptor> interceptors)
    {
        this.unitOfWork = unitOfWork;
        this.interceptors = interceptors;
    }

    public string? ContextKey => this.unitOfWork.ContextKey;

    public IDbConnection Connection => this.unitOfWork.Connection;

    public IDbTransaction? Transaction => this.unitOfWork.Transaction;

    public UnitOfWorkStatus Status => this.unitOfWork.Status;

    public void Begin()
    {
        foreach (var interceptor in this.interceptors)
        {
            interceptor.TransactionStarting(this);
        }

        this.unitOfWork.Begin();

        foreach (var interceptor in this.interceptors)
        {
            interceptor.TransactionStarted(this);
        }
    }

    public async Task BeginAsync(CancellationToken cancellationToken = default)
    {
        foreach (var interceptor in this.interceptors)
        {
            await interceptor.TransactionStartingAsync(this, cancellationToken);
        }

        await this.unitOfWork.BeginAsync(cancellationToken);

        foreach (var interceptor in this.interceptors)
        {
            await interceptor.TransactionStartedAsync(this, cancellationToken);
        }
    }

    public void Commit()
    {
        foreach (var interceptor in this.interceptors)
        {
            interceptor.TrnasactionCommitting(this);
        }

        this.unitOfWork.Commit();

        foreach (var interceptor in this.interceptors)
        {
            interceptor.TransactionCommitted(this);
        }
    }

    public async Task CommitAsync(CancellationToken cancellationToken = default)
    {
        foreach (var interceptor in this.interceptors)
        {
            await interceptor.TransactionCommitingAsync(this, cancellationToken);
        }

        await this.unitOfWork.CommitAsync(cancellationToken);

        foreach (var interceptor in this.interceptors)
        {
            await interceptor.TransactionCommitedAsync(this, cancellationToken);
        }
    }

    public void Rollback()
    {
        foreach (var interceptor in this.interceptors)
        {
            interceptor.TransactionRollingBack(this);
        }

        this.unitOfWork.Rollback();

        foreach (var interceptor in this.interceptors)
        {
            interceptor.TransactionRolledBack(this);
        }
    }

    public async Task RollbackAsync(CancellationToken cancellationToken = default)
    {
        foreach (var interceptor in this.interceptors)
        {
            await interceptor.TransactionRollingBackAsync(this, cancellationToken);
        }

        await this.unitOfWork.RollbackAsync(cancellationToken);

        foreach (var interceptor in this.interceptors)
        {
            await interceptor.TransactionRolledBackAsync(this, cancellationToken);
        }
    }

    public void Dispose()
    {
        this.unitOfWork.Dispose();
    }

    public ValueTask DisposeAsync()
    {
        return this.unitOfWork.DisposeAsync();
    }
}
