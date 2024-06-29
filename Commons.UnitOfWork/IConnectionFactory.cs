using System.Data;

namespace Commons.UnitOfWork
{
    public interface IConnectionFactory
    {
        IDbConnection Open();

        Task<IDbConnection> OpenAsync(CancellationToken cancellationToken = default);
    }
}
