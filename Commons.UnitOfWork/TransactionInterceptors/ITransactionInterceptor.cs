namespace Commons.UnitOfWork.TransactionInterceptors;

public interface ITransactionInterceptor
{
    void TransactionStarting(IUnitOfWork unitOfWork);
    Task TransactionStartingAsync(IUnitOfWork unitOfWork, CancellationToken cancellationToken = default);

    void TransactionStarted(IUnitOfWork unitOfWork);
    Task TransactionStartedAsync(IUnitOfWork unitOfWork, CancellationToken cancellationToken = default);

    void TrnasactionCommitting(IUnitOfWork unitOfWork);
    Task TransactionCommitingAsync(IUnitOfWork unitOfWork, CancellationToken cancellationToken = default);

    void TransactionCommitted(IUnitOfWork unitOfWork);
    Task TransactionCommitedAsync(IUnitOfWork unitOfWork, CancellationToken cancellationToken = default);

    void TransactionRollingBack(IUnitOfWork unitOfWork);
    Task TransactionRollingBackAsync(IUnitOfWork unitOfWork, CancellationToken cancellationToken = default);

    void TransactionRolledBack(IUnitOfWork unitOfWork);
    Task TransactionRolledBackAsync(IUnitOfWork unitOfWork, CancellationToken cancellationToken = default);
}
