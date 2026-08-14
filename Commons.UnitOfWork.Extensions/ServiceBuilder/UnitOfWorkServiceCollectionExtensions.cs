using Commons.Database.ConnectionFactory;
using Commons.UnitOfWork.Context;
using Commons.UnitOfWork.Extensions.Contexts;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Data;

namespace Commons.UnitOfWork.Extensions;

public static class UnitOfWorkServiceCollectionExtensions
{
    public static IUnitOfWorkServiceBuilder AddUnitOfWork(this IServiceCollection services)
    {
        var databaseContexts = new Dictionary<string, DatabaseContextOptions>();
        services.TryAddTransient<IConnectionFactory>(serviceProvider => {
            return new DefaultConnectionFactory(databaseContexts);
        });
        services.TryAddTransient<IUnitOfWorkFactory, DefaultUnitOfWorkFactory>();
        
        services.TryAddScoped<IMutableUnitOfWorkContext, DefaultUnitOfWorkContext>();
        services.TryAddScoped<IUnitOfWorkContext>(provider =>
            provider.GetRequiredService<IMutableUnitOfWorkContext>());
        services.TryAddScoped<IConnectionContext, DefaultConnectionContext>();
        services.TryAddScoped<ITransactionContext, DefaultTransactionContext>();
        
        return new DefaultUnitOfWorkServiceBuilder(services, databaseContexts);
    }
}
