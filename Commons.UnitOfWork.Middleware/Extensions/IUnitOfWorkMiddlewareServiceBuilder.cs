using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Data;

namespace Commons.UnitOfWork.Middleware.Extensions;

public interface IUnitOfWorkMiddlewareServiceBuilder
{
    IUnitOfWorkMiddlewareServiceBuilder SetOptions(IConfigurationSection configurationSection);
    IUnitOfWorkMiddlewareServiceBuilder SetDefaultIsolationLevel(IsolationLevel isolationLevel);
}
