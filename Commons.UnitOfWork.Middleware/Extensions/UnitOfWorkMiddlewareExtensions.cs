using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Commons.UnitOfWork.Middleware.Extensions
{
    public static class UnitOfWorkMiddlewareExtensions
    {
        public static IUnitOfWorkMiddlewareServiceBuilder SetupUnitOfWorkMiddleware(this IServiceCollection services)
        {
            return new DefaultUnitOfWorkMiddlewareServiceBuilder(services);
        }

        public static IApplicationBuilder UseUnitOfWorkMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<UnitOfWorkMiddleware>();
            return app;
        }
    }
}
