using Commons.UnitOfWork.Context;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace Commons.UnitOfWork.Middleware;

public class UnitOfWorkMiddleware : IMiddleware
{
    private readonly UnitOfWorkMiddlewareOptions options;
    private readonly IUnitOfWorkFactory unitOfWorkFactory;
    private readonly IMutableUnitOfWorkContext unitOfWorkContext;

    public UnitOfWorkMiddleware(
        IOptions<UnitOfWorkMiddlewareOptions> options,
        IUnitOfWorkFactory unitOfWorkFactory,
        IMutableUnitOfWorkContext unitOfWorkContext)
    {
        this.options = options.Value;
        this.unitOfWorkFactory = unitOfWorkFactory;
        this.unitOfWorkContext = unitOfWorkContext;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        var endpoint = context.GetEndpoint();
        var databaseContextAttribute = endpoint?.Metadata.GetMetadata<DatabaseContextAttribute>();
        var databaseContextKey = databaseContextAttribute?.DatabaseContextKey ?? string.Empty;

        await using (var unitOfWork = await this.unitOfWorkFactory.CreateAsync(
            this.options.IsolationLevel,
            databaseContextKey,
            context.RequestAborted))
        {
            this.unitOfWorkContext.Current = unitOfWork;
            await unitOfWork.BeginAsync(context.RequestAborted);
            await next(context);
            await unitOfWork.CommitAsync(context.RequestAborted);
        }
    }
}
