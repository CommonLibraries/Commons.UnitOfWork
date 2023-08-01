using System.Data;
using System.Data.Common;

namespace Commons.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IConnectionContext connectionContext;
        private readonly ITransactionContext transactionContext;

        private bool disposed;

        public UnitOfWork(IConnectionContext connectionContext, ITransactionContext transactionContext)
        {
            this.connectionContext = connectionContext ?? 
                throw new ArgumentNullException(nameof(connectionContext), "ConnectionContext is must be NOT null.");
            this.transactionContext = transactionContext ??
                throw new ArgumentNullException(nameof(transactionContext), "TransactionContext is must be NOT null.");
        }

        public bool IsDisposed => this.disposed;

        public void Commit()
        {
            var transaction = this.transactionContext.GetTransaction() ?? 
                throw new InvalidOperationException("Transaction have not been begun yet.");
            
            transaction.Commit();
        }

        public async Task CommitAsync(CancellationToken cancellationToken = default)
        {
            var transaction = this.transactionContext.GetTransaction() ?? 
                throw new InvalidOperationException("Transaction have not been begun yet.");

            if (transaction is not DbTransaction dbTransaction) {
                throw new NotSupportedException(
                    $"The transaction type does not implement {typeof(DbTransaction).FullName}.",
                    new MissingMethodException("The transaction type does not have CommitAsync method."));
            }

            await dbTransaction.CommitAsync(cancellationToken);
        }

        public void Rollback()
        {
            var transaction = this.transactionContext.GetTransaction() ??
                throw new InvalidOperationException("Transaction have not been begun yet.");
            
            transaction.Rollback();
        }

        public async Task RollbackAsync(CancellationToken cancellationToken = default)
        {
            var transaction = this.transactionContext.GetTransaction() ?? 
                throw new InvalidOperationException("Transaction have not been begun yet.");
            
            if (transaction is not DbTransaction dbTransaction) {
                throw new NotSupportedException(
                    $"The transaction type does not implement {typeof(DbTransaction).FullName}.",
                    new MissingMethodException("The transaction type does not have RollbackAsync method."));
            }

            await dbTransaction.RollbackAsync(cancellationToken);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    var transaction = this.transactionContext.GetTransaction();
                    transaction?.Dispose();
                    this.transactionContext.SetTransaction(null!);
                }

                disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        protected virtual async ValueTask DisposeAsyncCore() {
            if (this.transactionContext.GetTransaction() is IAsyncDisposable disposable) {
                await disposable.DisposeAsync();
                this.transactionContext.SetTransaction(null!);
            }
        }

        public async ValueTask DisposeAsync()
        {
            await DisposeAsyncCore().ConfigureAwait(false);

            Dispose(disposing: false);
            GC.SuppressFinalize(this);
        }
    }
}
