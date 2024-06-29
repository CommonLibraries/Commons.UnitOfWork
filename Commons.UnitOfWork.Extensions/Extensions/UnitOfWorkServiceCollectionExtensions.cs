using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Data;

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
