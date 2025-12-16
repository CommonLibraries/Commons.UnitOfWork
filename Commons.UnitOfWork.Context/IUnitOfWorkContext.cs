using System.Data.Common;

namespace Commons.UnitOfWork.Context
{
    public interface IUnitOfWorkContext
    {
        IUnitOfWork Current { get; }
    }
}
