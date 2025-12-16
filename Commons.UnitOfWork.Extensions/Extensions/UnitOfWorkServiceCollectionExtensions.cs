using Commons.Database.ConnectionFactory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Data;

namespace Commons.UnitOfWork.Extensions
{
    public static class UnitOfWorkServiceCollectionExtensions
    {
        public static IUnitOfWorkServiceBuilder AddUnitOfWork(this IServiceCollection services)
        {
            var databaseContexts = new Dictionary<string, DatabaseContextOptions>();
            services.TryAddTransient<IConnectionFactory>(serviceProvider => {
                return new DefaultConnectionFactory(databaseContexts);
            });
            services.TryAddTransient<IUnitOfWorkFactory, DefaultUnitOfWorkFactory>();
            return new DefaultUnitOfWorkServiceBuilder(services, databaseContexts);
        }
    }
}
