using Commons.UnitOfWork.Context;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Commons.UnitOfWork.Middleware
{
    public class UnitOfWorkMiddleware : IMiddleware
    {
        private readonly UnitOfWorkMiddlewareOptions options;
        private readonly IUnitOfWorkFactory unitOfWorkFactory;
        private readonly IMutableUnitOfWorkContext unitOfWorkContext;
        private readonly IMutableConnectionContext connectionContext;
        private readonly IMutableTransactionContext transactionContext;

        public UnitOfWorkMiddleware(
            IOptions<UnitOfWorkMiddlewareOptions> options,
            IUnitOfWorkFactory unitOfWorkFactory,
            IMutableUnitOfWorkContext unitOfWorkContext,
            IMutableConnectionContext connectionContext,
            IMutableTransactionContext transactionContext)
        {
            this.options = options.Value;
            this.unitOfWorkFactory = unitOfWorkFactory;
            this.unitOfWorkContext = unitOfWorkContext;
            this.connectionContext = connectionContext;
            this.transactionContext = transactionContext;
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
                this.connectionContext.Current = unitOfWork.Connection;

                await unitOfWork.BeginAsync(context.RequestAborted);
                this.transactionContext.Current = unitOfWork.Transaction;

                await next(context);

                await unitOfWork.CommitAsync(context.RequestAborted);
            }
        }
    }
}
