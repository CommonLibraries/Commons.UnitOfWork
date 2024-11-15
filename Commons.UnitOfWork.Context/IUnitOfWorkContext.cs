using System.Data.Common;

namespace Commons.UnitOfWork.Context
{
    public interface IUnitOfWorkContext : IDisposable, IAsyncDisposable
    {
        IUnitOfWork Current { get; }
    }
}
