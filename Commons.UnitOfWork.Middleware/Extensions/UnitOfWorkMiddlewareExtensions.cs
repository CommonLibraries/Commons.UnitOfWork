using Commons.UnitOfWork.Context;
using Commons.UnitOfWork.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Commons.UnitOfWork.Middleware.Extensions;

public static class UnitOfWorkMiddlewareExtensions
{
    public static IUnitOfWorkMiddlewareServiceBuilder AddUnitOfWorkMiddleware(this IUnitOfWorkServiceBuilder unitOfWorkServiceBuilder)
    {
        unitOfWorkServiceBuilder.Services.AddTransient<UnitOfWorkMiddleware>();
        return new DefaultUnitOfWorkMiddlewareServiceBuilder(unitOfWorkServiceBuilder.Services);
    }

    public static IApplicationBuilder UseUnitOfWorkMiddleware(this IApplicationBuilder app)
    {
        app.UseMiddleware<UnitOfWorkMiddleware>();
        return app;
    }
}
