using Microsoft.Extensions.DependencyInjection;

namespace Commons.UnitOfWork.Extensions
{
    public static class UnitOfWorkServiceCollectionExtensions
    {
        public static IUnitOfWorkServiceBuilder AddUnitOfWork(this IServiceCollection services)
        {
            return new DefaultUnitOfWorkServiceBuilder(services);
        }
    }
}
