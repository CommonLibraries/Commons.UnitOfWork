﻿using Commons.UnitOfWork.Context;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Data;

namespace Commons.UnitOfWork.Middleware.Extensions
{
    internal class DefaultUnitOfWorkMiddlewareServiceBuilder : IUnitOfWorkMiddlewareServiceBuilder
    {
        private readonly IServiceCollection services;

        public DefaultUnitOfWorkMiddlewareServiceBuilder(IServiceCollection services)
        {
            this.services = services;
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

        public IServiceCollection Done()
        {
            services.TryAddScoped<DefaultUnitOfWorkScope>();
            services.TryAddScoped<IUnitOfWorkContext>(serviceProvider =>
            {
                return serviceProvider.GetRequiredService<DefaultUnitOfWorkScope>();
            });
            services.TryAddScoped<IConnectionContext, DefaultConnectionScope>();
            services.TryAddScoped<ITransactionContext, DefaultTransactionScope>();
            services.AddTransient<UnitOfWorkMiddleware>();
            return services;
        }
    }
}
