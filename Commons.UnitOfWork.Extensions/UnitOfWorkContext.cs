using Microsoft.Extensions.DependencyInjection;
using System.Data;
using System.Data.Common;

namespace Commons.UnitOfWork
{
    public class UnitOfWorkContext : IUnitOfWorkContext
    {
        private IUnitOfWork? unitOfWork;

        private readonly IConnectionContext connectionContext;
        private readonly ITransactionContext transactionContext;

        public UnitOfWorkContext(IConnectionContext connectionContext, ITransactionContext transactionContext)
        {
            this.connectionContext = connectionContext;
            this.transactionContext = transactionContext;
        }

        public IUnitOfWork Create(IsolationLevel isolationLevel)
        {
            if (this.unitOfWork is not null && !this.unitOfWork.IsDisposed)
            {
                throw new InvalidOperationException("There is an existing Unit of Work.");
            }

            if (this.connectionContext.GetConnection() is not DbConnection connection)
            {
                throw new InvalidCastException($"The connection does not inherit {nameof(DbConnection)} class.");
            }

            var transaction = connection.BeginTransaction(isolationLevel);
            this.transactionContext.SetTransaction(transaction);

            this.unitOfWork = new UnitOfWork(this.connectionContext, this.transactionContext);
            return this.unitOfWork;
        }

        public async Task<IUnitOfWork> CreateAsync(IsolationLevel isolationLevel, CancellationToken cancellationToken = default)
        {
            if (await this.connectionContext.GetConnectionAsync(cancellationToken)
                        is not DbConnection connection) {
                throw new InvalidCastException($"The connection does not inherit {nameof(DbConnection)} class.");
            }

            var transaction = await connection.BeginTransactionAsync(isolationLevel, cancellationToken);
            this.transactionContext.SetTransaction(transaction);
            
            this.unitOfWork = new UnitOfWork(this.connectionContext, this.transactionContext);
            return this.unitOfWork;
        }
    }
}
