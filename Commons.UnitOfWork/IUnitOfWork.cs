using System.Data;

namespace Commons.UnitOfWork
{
    public interface IUnitOfWork : IDisposable, IAsyncDisposable
    {
        IDbConnection Conection { get; }
        IDbTransaction Transaction { get; }
        IDbCommand CreateCommand(string commandText = "");

        void Begin(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted);
        Task BeginAsync(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted, CancellationToken cancellationToken = default);

        void Commit();
        void CommitAsync(CancellationToken cancellationToken = default);

        void Rollback();
        void RollbackAsync(CancellationToken cancellationToken = default);
    }
}
