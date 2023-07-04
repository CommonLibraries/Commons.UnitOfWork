using System.Data;

namespace Commons.UnitOfWork
{
    public interface IConnectionContext
    {
        IDbConnection GetConnection();

        Task<IDbConnection> GetConnectionAsync(CancellationToken cancellationToken = default);
    }
}
