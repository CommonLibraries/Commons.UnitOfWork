using Commons.UnitOfWork.Scope;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace Commons.UnitOfWork.Middleware
{
    internal class UnitOfWorkMiddleware : IMiddleware
    {
        private readonly DefaultUnitOfWorkScope scopedUnitOfWork;
        private readonly IUnitOfWorkFactory unitOfWorkFactory;
        private readonly UnitOfWorkMiddlewareOptions options;
        public UnitOfWorkMiddleware(
            DefaultUnitOfWorkScope scopedUnitOfWork,
            IUnitOfWorkFactory unitOfWorkFactory,
            IOptions<UnitOfWorkMiddlewareOptions> options)
        {
            this.scopedUnitOfWork = scopedUnitOfWork;
            this.unitOfWorkFactory = unitOfWorkFactory;
            this.options = options.Value;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            var unitOfWork = await this.unitOfWorkFactory.CreateAsync(this.options.IsolationLevel, context.RequestAborted);
            this.scopedUnitOfWork.Current = unitOfWork;

            await unitOfWork.BeginAsync(context.RequestAborted);
            await next(context);
            await unitOfWork.CommitAsync(context.RequestAborted);
        }
    }
}
