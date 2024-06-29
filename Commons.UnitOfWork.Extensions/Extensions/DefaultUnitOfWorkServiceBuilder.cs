using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Commons.UnitOfWork.Extensions
{
    public class DefaultUnitOfWorkServiceBuilder : IUnitOfWorkServiceBuilder
    {
        protected IServiceCollection services;
        
        protected string invariantName = null!;
        protected string connectionString = null!;
        
        public DefaultUnitOfWorkServiceBuilder(IServiceCollection services)
        {
            this.services = services;
        }

        public IUnitOfWorkServiceBuilder SetConnectionString(string connectionString)
        {
            this.connectionString = connectionString;
            return this;
        }

        public IUnitOfWorkServiceBuilder SetInvariantName(string invariantName)
        {
            this.invariantName = invariantName;
            return this;
        }

        public IServiceCollection Done()
        {
            services.TryAddTransient<IConnectionFactory>(serviceProvider => {
                return new DefaultConnectionFactory(this.invariantName, this.connectionString);
            });
            services.TryAddTransient<IUnitOfWorkFactory, DefaultUnitOfWorkFactory>();
            return services;
        }
    }
}
