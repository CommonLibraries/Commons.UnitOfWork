using Commons.Database.ConnectionFactory;
using Commons.UnitOfWork.TransactionInterceptors;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Commons.UnitOfWork.Extensions;

public class DefaultUnitOfWorkServiceBuilder : IUnitOfWorkServiceBuilder
{
    protected IServiceCollection services;
    protected IDictionary<string, DatabaseContextOptions> databaseContexts;

    public DefaultUnitOfWorkServiceBuilder(
        IServiceCollection services,
        IDictionary<string, DatabaseContextOptions> databaseContexts)
    {
        this.services = services;
        this.databaseContexts = databaseContexts;
    }

    public IServiceCollection Services => this.services;

    public IUnitOfWorkServiceBuilder AddDatabaseContext(string databaseContextKey, string invariantName, string connectionString)
    {
        this.databaseContexts.Add(databaseContextKey, new DatabaseContextOptions
        {
            InvariantName = invariantName,
            ConnectionString = connectionString
        });
        return this;
    }

    public IUnitOfWorkServiceBuilder AddTransactionInterceptor<TInterceptor>() where TInterceptor : class, ITransactionInterceptor
    {
        this.services.AddTransient<ITransactionInterceptor, TInterceptor>();
        return this;
    }
}
