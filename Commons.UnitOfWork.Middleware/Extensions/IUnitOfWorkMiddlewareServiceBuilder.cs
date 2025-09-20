using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Data;

namespace Commons.UnitOfWork.Middleware.Extensions
{
    public interface IUnitOfWorkMiddlewareServiceBuilder
    {
        IUnitOfWorkMiddlewareServiceBuilder SetDefaultIsolationLevel(IsolationLevel isolationLevel);
        IUnitOfWorkMiddlewareServiceBuilder SetOptions(IConfigurationSection configurationSection);
    }
}
