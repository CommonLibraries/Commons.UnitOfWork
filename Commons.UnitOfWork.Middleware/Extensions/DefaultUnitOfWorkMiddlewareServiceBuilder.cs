using Commons.Database.ConnectionFactory;
using Commons.UnitOfWork.Context;
using Commons.UnitOfWork.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Data;

namespace Commons.UnitOfWork.Middleware.Extensions
{
    internal class DefaultUnitOfWorkMiddlewareServiceBuilder : IUnitOfWorkMiddlewareServiceBuilder
    {
        private readonly IServiceCollection services;
        private readonly IUnitOfWorkServiceBuilder unitOfWorkServiceBuilder;

        public DefaultUnitOfWorkMiddlewareServiceBuilder(
            IServiceCollection services,
            IUnitOfWorkServiceBuilder unitOfWorkServiceBuilder)
        {
            this.services = services;
            this.unitOfWorkServiceBuilder = unitOfWorkServiceBuilder;
        }
        
        public IUnitOfWorkMiddlewareServiceBuilder SetOptions(IConfigurationSection configurationSection)
        {
            this.services.AddOptions<UnitOfWorkMiddlewareOptions>()
                .Bind(configurationSection);
            return this;
        }

        public IUnitOfWorkMiddlewareServiceBuilder SetDefaultIsolationLevel(IsolationLevel isolationLevel)
        {
            this.services.PostConfigure<UnitOfWorkMiddlewareOptions>(options =>
            {
                options.IsolationLevel = isolationLevel;
            });

            return this;
        }

        public IUnitOfWorkMiddlewareServiceBuilder AddDatabaseContext(string databaseContextKey, string invariantName, string connectionString)
        {
            this.unitOfWorkServiceBuilder.AddDatabaseContext(databaseContextKey, invariantName, connectionString);
            return this;
        }
    }
}
