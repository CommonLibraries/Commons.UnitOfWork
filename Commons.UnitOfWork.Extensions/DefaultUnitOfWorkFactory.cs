using Commons.Database.ConnectionFactory;
using Commons.UnitOfWork.Extensions.TransactionInterceptors;
using Commons.UnitOfWork.TransactionInterceptors;
using System.Data;
using System.Data.Common;

namespace Commons.UnitOfWork;

internal class DefaultUnitOfWorkFactory : IUnitOfWorkFactory
{
    private readonly IConnectionFactory connectionFactory;
    private readonly IList<ITransactionInterceptor> transactionInterceptors;

    public DefaultUnitOfWorkFactory(IConnectionFactory connectionFactory,
        IEnumerable<ITransactionInterceptor> transactionInterceptors)
    {
        this.connectionFactory = connectionFactory;
        this.transactionInterceptors = transactionInterceptors.ToList();
    }

    public IUnitOfWork Create(IsolationLevel isolationLevel, string? databaseContextKey = null)
    {
        if (this.connectionFactory.Open() is not DbConnection connection)
        {
            throw new InvalidCastException($"The connection does not inherit {nameof(DbConnection)} class.");
        }

        var unitOfWork = new DefaultUnitOfWork(connection, isolationLevel, databaseContextKey);
        var unitOfWorkWithTransactionInterceptors = new DefaultUnitOfWorkWithTransactionInterceptors(unitOfWork, this.transactionInterceptors);
        return unitOfWorkWithTransactionInterceptors;
    }

    public async Task<IUnitOfWork> CreateAsync(IsolationLevel isolationLevel, string? databaseContextKey = null, CancellationToken cancellationToken = default)
    {
        if (await this.connectionFactory.OpenAsync(databaseContextKey, cancellationToken)
                    is not DbConnection connection)
        {
            throw new InvalidCastException($"The connection does not inherit {nameof(DbConnection)} class.");
        }

        var unitOfWork = new DefaultUnitOfWork(connection, isolationLevel, databaseContextKey);
        var unitOfWorkWithTransactionInterceptors = new DefaultUnitOfWorkWithTransactionInterceptors(unitOfWork, this.transactionInterceptors);
        return unitOfWorkWithTransactionInterceptors;
    }
}
