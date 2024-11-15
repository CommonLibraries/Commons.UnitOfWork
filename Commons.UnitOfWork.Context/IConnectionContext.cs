using System.Data;

namespace Commons.UnitOfWork.Context
{
    public interface IConnectionContext
    {
        IDbConnection Current { get; }
    }
}
