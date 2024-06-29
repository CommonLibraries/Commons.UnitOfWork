using System.Data.Common;

namespace Commons.UnitOfWork.Scope
{
    public interface IUnitOfWorkScope : IDisposable, IAsyncDisposable
    {
        IUnitOfWork Current { get; }
    }
}
