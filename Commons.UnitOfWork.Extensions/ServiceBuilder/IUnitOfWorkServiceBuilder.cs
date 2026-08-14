using Commons.UnitOfWork.TransactionInterceptors;
using Microsoft.Extensions.DependencyInjection;

namespace Commons.UnitOfWork.Extensions;

public interface IUnitOfWorkServiceBuilder
{
    IServiceCollection Services { get; }
    IUnitOfWorkServiceBuilder AddDatabaseContext(string databaseContextKey, string invariantName, string connectionString);
    IUnitOfWorkServiceBuilder AddTransactionInterceptor<TInterceptor>() where TInterceptor : class, ITransactionInterceptor;
}
