using System.Data;

namespace Commons.UnitOfWork
{
    public interface IConnectionContext: IDisposable, IAsyncDisposable
    {
        IDbConnection GetConnection();

        Task<IDbConnection> GetConnectionAsync(CancellationToken cancellationToken = default);
    }
}
