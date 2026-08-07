using System.Data;
using System.Data.Common;

namespace Commons.UnitOfWork
{
    internal class DefaultUnitOfWork : IUnitOfWork
    {
        private IDbConnection? connection;
        private readonly IsolationLevel isolationLevel;
        private IDbTransaction? transaction;
        private UnitOfWorkStatus status = UnitOfWorkStatus.NotStarted;
        private readonly string? contextKey;

        public DefaultUnitOfWork(
            IDbConnection connection,
            IsolationLevel isolationLevel,
            string? contextKey = null)
        {
            this.contextKey = contextKey;
            if (connection is null)
            {
                throw new ArgumentNullException(nameof(connection), "Connection is must be NOT null.");
            }
            this.connection = connection;
            this.isolationLevel = isolationLevel;
        }

        public IDbConnection Connection => this.connection ?? throw new InvalidOperationException();
        public IDbTransaction? Transaction => this.transaction;
        public UnitOfWorkStatus Status => this.status;
        public string? ContextKey => this.contextKey;

        public void Begin()
        {
            if (this.connection is null)
            {
                throw new InvalidOperationException();
            }

            this.transaction = this.connection.BeginTransaction(this.isolationLevel);
            this.status = UnitOfWorkStatus.Active;
        }

        public async Task BeginAsync(CancellationToken cancellationToken = default)
        {
            if (this.connection is null) throw new InvalidOperationException();
            if (this.connection is not DbConnection connection)
            {
                throw new NotSupportedException();
            }

            this.transaction = await connection.BeginTransactionAsync(this.isolationLevel, cancellationToken);
            this.status = UnitOfWorkStatus.Active;
        }

        public void Commit()
        {
            if (this.transaction is null)
            {
                throw new InvalidOperationException("Transaction have not been begun yet.");
            }

            this.transaction.Commit();
            this.status = UnitOfWorkStatus.Committed;
        }

        public async Task CommitAsync(CancellationToken cancellationToken = default)
        {
            if (this.transaction is null)
            {
                throw new InvalidOperationException("Transaction have not been begun yet.");
            }

            if (this.transaction is not DbTransaction transaction)
            {
                throw new NotSupportedException(
                    $"The transaction type does not implement {typeof(DbTransaction).FullName}.",
                    new MissingMethodException("The transaction type does not have CommitAsync method."));
            }

            await transaction.CommitAsync(cancellationToken);
            this.status = UnitOfWorkStatus.Committed;
        }

        public void Rollback()
        {
            if (this.transaction is null)
            {
                throw new InvalidOperationException("Transaction have not been begun yet.");
            }

            this.transaction.Rollback();
            this.status = UnitOfWorkStatus.RolledBack;
        }

        public async Task RollbackAsync(CancellationToken cancellationToken = default)
        {
            if (this.transaction is null)
            {
                throw new InvalidOperationException("Transaction have not been begun yet.");
            }

            if (this.transaction is not DbTransaction transaction)
            {
                throw new NotSupportedException(
                    $"The transaction type does not implement {typeof(DbTransaction).FullName}.",
                    new MissingMethodException("The transaction type does not have RollbackAsync method."));
            }

            await transaction.RollbackAsync(cancellationToken);
            this.status = UnitOfWorkStatus.RolledBack;
        }

        private bool disposed;
        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    this.transaction?.Dispose();
                    this.transaction = null;
                    this.connection?.Dispose();
                    this.connection = null;
                }

                disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        protected virtual async ValueTask DisposeAsyncCore()
        {
            if (this.transaction is IAsyncDisposable transaction)
            {
                await transaction.DisposeAsync();
                this.transaction = null;
            }

            if (this.connection is IAsyncDisposable connection)
            {
                await connection.DisposeAsync();
                this.connection = null;
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
