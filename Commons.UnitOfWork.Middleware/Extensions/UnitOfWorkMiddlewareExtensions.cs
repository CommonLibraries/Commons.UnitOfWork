using Commons.UnitOfWork.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Commons.UnitOfWork.Middleware.Extensions
{
    public static class UnitOfWorkMiddlewareExtensions
    {
        public static IUnitOfWorkMiddlewareServiceBuilder AddUnitOfWorkMiddleware(this IServiceCollection services)
        {
            services.TryAddScoped<IUnitOfWorkContext, DefaultUnitOfWorkContext>();
            services.TryAddScoped<IConnectionContext, DefaultConnectionContext>();
            services.TryAddScoped<ITransactionContext, DefaultTransactionContext>();
            services.AddTransient<UnitOfWorkMiddleware>();
            
            return new DefaultUnitOfWorkMiddlewareServiceBuilder(services);
        }

        public static IApplicationBuilder UseUnitOfWorkMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<UnitOfWorkMiddleware>();
            return app;
        }
    }
}
