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
            services.TryAddScoped<IUnitOfWork>(serviceProvider =>
            {
                var uowFactory = serviceProvider.GetRequiredService<IUnitOfWorkFactory>();
                return uowFactory.Create();
            });
            services.TryAddSingleton<IUnitOfWorkFactory>((serviceProvider) =>
            {
                return new UnitOfWorkFactory(invariantName, connectionString);
            });
            return services;
        }
    }
}
