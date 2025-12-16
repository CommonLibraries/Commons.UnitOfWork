using Microsoft.Extensions.DependencyInjection;

namespace Commons.UnitOfWork.Extensions
{
    public interface IUnitOfWorkServiceBuilder
    {
        IUnitOfWorkServiceBuilder AddDatabaseContext(string databaseContextKey, string invariantName, string connectionString);
    }
}
