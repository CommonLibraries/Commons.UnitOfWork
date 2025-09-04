using Commons.UnitOfWork.Context;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace Commons.UnitOfWork.Middleware
{
    internal class UnitOfWorkMiddleware : IMiddleware
    {
        private readonly IUnitOfWorkContext unitOfWorkContext;
        private readonly IUnitOfWorkFactory unitOfWorkFactory;
        private readonly UnitOfWorkMiddlewareOptions options;
        public UnitOfWorkMiddleware(
            IUnitOfWorkContext unitOfWorkContext,
            IUnitOfWorkFactory unitOfWorkFactory,
            IOptions<UnitOfWorkMiddlewareOptions> options)
        {
            this.unitOfWorkContext = unitOfWorkContext;
            this.unitOfWorkFactory = unitOfWorkFactory;
            this.options = options.Value;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            await using (var unitOfWork = await this.unitOfWorkFactory.CreateAsync(
                this.options.IsolationLevel,
                context.RequestAborted))
            {
                var defaultUnitOfWorkContext = this.unitOfWorkContext as DefaultUnitOfWorkContext;
                if (defaultUnitOfWorkContext is not null)
                {
                    defaultUnitOfWorkContext.Current = unitOfWork;
                }

                await unitOfWork.BeginAsync(context.RequestAborted);
                await next(context);
                await unitOfWork.CommitAsync(context.RequestAborted);
            }
        }
    }
}
