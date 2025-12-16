using Commons.UnitOfWork.Context;
using Commons.UnitOfWork.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Commons.UnitOfWork.Middleware.Extensions
{
    public static class UnitOfWorkMiddlewareExtensions
    {
        public static IUnitOfWorkMiddlewareServiceBuilder AddUnitOfWorkMiddleware(this IServiceCollection services)
        {
            services.TryAddScoped<IMutableUnitOfWorkContext, DefaultUnitOfWorkContext>();
            services.TryAddScoped<IUnitOfWorkContext>(provider =>
                provider.GetRequiredService<IMutableUnitOfWorkContext>());

            services.TryAddScoped<IMutableConnectionContext, DefaultConnectionContext>();
            services.TryAddScoped<IConnectionContext>(provider =>
                provider.GetRequiredService<IMutableConnectionContext>());

            services.TryAddScoped<IMutableTransactionContext, DefaultTransactionContext>();
            services.TryAddScoped<ITransactionContext>(provider =>
                provider.GetRequiredService<IMutableTransactionContext>());

            services.AddTransient<UnitOfWorkMiddleware>();

            var unitOfWorkServiceBuilder = services.AddUnitOfWork();

            return new DefaultUnitOfWorkMiddlewareServiceBuilder(services, unitOfWorkServiceBuilder);
        }

        public static IApplicationBuilder UseUnitOfWorkMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<UnitOfWorkMiddleware>();
            return app;
        }
    }
}
