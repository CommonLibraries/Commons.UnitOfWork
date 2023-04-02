using System.Data;
using System.Data.Common;

namespace Commons.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        public IDbConnection Connection { get; protected set; }

        public IDbTransaction Transaction { get; protected set; }

        public UnitOfWork(IDbConnection connection)
        {
            this.Connection = connection;
            if (this.Connection is null)
            {
                throw new ArgumentNullException(nameof(connection), "Connection is must be NOT null.");
            }
            this.Transaction = null!;
        }

        public IDbCommand CreateCommand(string commandText = "")
        {
            var command = this.Connection.CreateCommand();
            command.Transaction = this.Transaction;
            command.CommandText = commandText;
            return command;
        }

        public void Begin(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted)
        {
            this.Transaction = this.Connection.BeginTransaction(isolationLevel);
        }

        public async Task BeginAsync(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted, CancellationToken cancellationToken = default)
        {
            var connection = this.Connection as DbConnection;
            if (connection is null)
            {
                throw new NotSupportedException(
                    $"The connection data type does not implement {typeof(DbConnection).FullName}.",
                    new MissingMethodException("BeginTransactionAsync", this.Connection.GetType().Name));
            }

            await connection.BeginTransactionAsync(isolationLevel, cancellationToken);
        }

        public void Commit()
        {
            this.Transaction.Commit();
        }

        public async Task CommitAsync(CancellationToken cancellationToken = default)
        {
            if (this.Transaction is null)
            {
                throw new NullReferenceException("Transaction have not been created yet.");
            }

            var transaction = this.Transaction as DbTransaction;
            if (transaction is null)
            {
                throw new NotSupportedException(
                    $"The transaction data type does not implement {typeof(DbTransaction).FullName}.",
                    new MissingMethodException("CommitAsync", this.Transaction.GetType().Name));
            }

            await transaction.CommitAsync(cancellationToken);
        }

        public void Rollback()
        {
            this.Transaction.Rollback();
        }

        public async Task RollbackAsync(CancellationToken cancellationToken = default)
        {
            if (this.Transaction is null)
            {
                throw new NullReferenceException("Transaction have not been created yet.");
            }

            var transaction = this.Transaction as DbTransaction;
            if (transaction is null)
            {
                throw new NotSupportedException(
                    $"The transaction data type does not implement {typeof(DbTransaction).FullName}.",
                    new MissingMethodException("RollbackAsync", this.Transaction.GetType().Name));
            }

            await transaction.RollbackAsync(cancellationToken);
        }

        private bool disposed;
        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
            {
                return;
            }

            if (disposing)
            {
                if (this.Transaction is not null)
                {
                    this.Transaction.Dispose();
                }

                this.Connection.Dispose();
            }

            // TODO: free unmanaged resources (unmanaged objects) and override finalizer
            // TODO: set large fields to null
            disposed = true;
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~UnitOfWork()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        public async ValueTask DisposeAsync()
        {
            await this.DisposeAsyncCore();
            this.Dispose(false);
            GC.SuppressFinalize(this);
        }

        protected virtual async ValueTask DisposeAsyncCore()
        {
            if (this.disposed)
            {
                return;
            }

            if (this.Transaction is not null)
            {
                var transaction = this.Transaction as DbTransaction;
                if (transaction is not null)
                {
                    await transaction.DisposeAsync();
                }
                else
                {
                    this.Transaction.Dispose();
                }
            }

            var connection = this.Connection as DbConnection;
            if (connection is not null)
            {
                await connection.DisposeAsync();
            }
            else
            {
                this.Connection.Dispose();
            }

            this.Transaction = null!;
            this.Connection = null!;
            this.disposed = true;
        }
    }
}
