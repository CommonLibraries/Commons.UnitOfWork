using Microsoft.Extensions.DependencyInjection;

namespace Commons.UnitOfWork.Extensions
{
    public interface IUnitOfWorkServiceBuilder
    {
        IUnitOfWorkServiceBuilder SetInvariantName(string invariantName);
        IUnitOfWorkServiceBuilder SetConnectionString(string connectionString);
        IServiceCollection Done();
    }
}
