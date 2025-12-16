using System.Data;

namespace Commons.UnitOfWork
{
    public interface IUnitOfWorkFactory
    {
        IUnitOfWork Create(
            IsolationLevel isolationLevel,
            string? databaseContextKey = null);
        Task<IUnitOfWork> CreateAsync(
            IsolationLevel isolationLevel,
            string? databaseContextKey = null,
            CancellationToken cancellationToken = default);
    }
}
