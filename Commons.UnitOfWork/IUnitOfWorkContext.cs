using System.Data;

namespace Commons.UnitOfWork
{
    public interface IUnitOfWorkContext
    {
        IUnitOfWork Create(IsolationLevel isolationLevel);
        Task<IUnitOfWork> CreateAsync(IsolationLevel isolationLevel, CancellationToken cancellationToken = default);
    }
}
