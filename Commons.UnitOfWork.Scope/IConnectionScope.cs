using System.Data;

namespace Commons.UnitOfWork.Scope
{
    public interface IConnectionScope
    {
        IDbConnection Current { get; }
    }
}
